using Amazon.S3;
using Amazon.S3.Model;
using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegracaoController : ControllerBase
    {

        private readonly BackBlazeConfig _config;

        public IntegracaoController(IOptions<BackBlazeConfig> config)
        {
            _config = config.Value;
        }


        [HttpGet("ObterURL")]
        public async Task<IntegracaoBackBlazeResponse> ObterURLTemporaria(string fileName)
        {
            using var client = new HttpClient();

            // Monta o header Authorization com Base64 (Basic Auth)
            var credentials = $"{_config.KeyId}:{_config.MasterKey}";
            var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            var response = await client.GetAsync(_config.AuthorizeAccountURL);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro na autenticação B2: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<IntegracaoBackBlazeResponse>(json, options);

            return result;

        }

    }
}