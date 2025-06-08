using APIGallery.Models;
using APIGallery.Models.BackBlaze;
using APIGallery.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime;
using System.Text.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
using APIGallery.DTO;
using APIGallery.Services.Interfaces;

namespace APIGallery.Services
{
    public class IntegrationBackBlazeService : IIntegrationBackBlazeService
    {
        private readonly BackBlazeSettings _settings;
        public IntegrationBackBlazeService(IOptions<BackBlazeSettings> settings, IWorkArtService workArtService)
        {
            _settings = settings.Value;
        }
        public async Task<ResponseDTO<BackBlazeAuthResponse>> GetAuthAsync()
        {
            try
            {
                string keyId = _settings.KeyId;
                string masterKey = _settings.MasterKey;
                string apiUrl = _settings.ApiUrlBase;

                var credentials = $"{keyId}:{masterKey}";
                var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

                var response = await client.GetAsync($"{apiUrl}/b2_authorize_account");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro na autenticação B2: {response.StatusCode} - {error}");
                }

                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                BackBlazeAuthResponse result = JsonSerializer.Deserialize<BackBlazeAuthResponse>(json, options);

                return new ResponseDTO<BackBlazeAuthResponse>
                {
                    Success = true,
                    Message = "Autenticação realizada com sucesso.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<BackBlazeAuthResponse>
                {
                    Success = false,
                    Message = "Erro na autenticação B2 backblaze: " + ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseDTO<BackBlazeGetUrlResponse>> GetUploadUrlAsync()
        {
            var authResponse = await GetAuthAsync();

            if (!authResponse.Success || authResponse.Data == null)
            {
                throw new Exception(authResponse.Message);
            }

            var auth = authResponse.Data;

            var request = new HttpRequestMessage(HttpMethod.Get, $"{auth.ApiUrl}/b2api/v2/b2_get_upload_url?bucketId={_settings.BucketId}");

            request.Headers.TryAddWithoutValidation("Authorization", auth.AuthorizationToken);

            HttpClient client = new HttpClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error during GET upload URL: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            BackBlazeGetUrlResponse result = JsonSerializer.Deserialize<BackBlazeGetUrlResponse>(json, options);

            return new ResponseDTO<BackBlazeGetUrlResponse>
            {
                Success = true,
                Message = "Get URLUpload sucessful",
                Data = result
            };
        }

        public async Task<ResponseDTO<BackBlazeUploadResponse>> UploadFileAsync(string fileName, Stream content, string contentType)
        {
            try
            {
                var uploadResponse = await GetUploadUrlAsync();

                if (!uploadResponse.Success || uploadResponse.Data == null)
                {
                    throw new Exception($"Error during get uploadURL: {uploadResponse.Message}");
                }

                BackBlazeGetUrlResponse backBlazeConfigs = uploadResponse.Data;

                string sha1Hash;
                byte[] fileBytes;
                using var stream = new MemoryStream();

                await content.CopyToAsync(stream);
                fileBytes = stream.ToArray();
                using var sha1 = SHA1.Create();
                var hashBytes = sha1.ComputeHash(fileBytes);
                sha1Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                string fileNameUTF8 = Uri.EscapeDataString(fileName);

                var request = new HttpRequestMessage(HttpMethod.Post, backBlazeConfigs.UploadUrl);

                request.Headers.TryAddWithoutValidation("Authorization", backBlazeConfigs.AuthorizationToken);
                request.Headers.TryAddWithoutValidation("Content-Type", "b2/x-auto");
                request.Headers.TryAddWithoutValidation("X-Bz-File-Name", fileNameUTF8);
                request.Headers.TryAddWithoutValidation("X-Bz-Content-Sha1", sha1Hash);
                request.Content = new ByteArrayContent(fileBytes);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                HttpClient client = new HttpClient();

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error on upload: {response.StatusCode} - {error}");
                }

                string publicUrl = $"{_settings.BucketName}/{fileNameUTF8}";

                var json = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(json);
                var fileId = document.RootElement.GetProperty("fileId").GetString();

                return new ResponseDTO<BackBlazeUploadResponse>
                {
                    Success = true,
                    Message = "Upload sucessful.",
                    Data = new BackBlazeUploadResponse
                    {
                        FileId = fileId,
                        PublicUrl = publicUrl
                    }
                };
            }
            catch (Exception ex)
            {
                var responseDTO = new ResponseDTO<BackBlazeUploadResponse>
                {
                    Success = false,
                    Message = "Error during upload" + ex,
                    Data = null
                };

                return responseDTO;
            }

        }


    }
}
