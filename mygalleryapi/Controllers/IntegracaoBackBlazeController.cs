using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using APIGallery.Models.BackBlaze;
using APIGallery.Models;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegracaoBackBlazeController : ControllerBase
    {
        private readonly BackBlazeSettings _settings;
        public IntegracaoBackBlazeController(IOptions<BackBlazeSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet("ObterAutorizacao")]
        public async Task<IActionResult> ObterAutorizacao(int id)
        {

            string keyId = _settings.KeyId;
            string masterKey = _settings.MasterKey;
            string apiUrl = _settings.ApiUrl;

            using var client = new HttpClient();

            var credentials = $"{keyId}:{masterKey}";
            var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            var response = await client.GetAsync($"{apiUrl}/b2_authorize_account");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro na autenticação B2: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<BackBlazeAuthResponse>(json, options);

            if (result == null)
                return StatusCode(500, new { error = "Falha ao desserializar resposta da BackBlaze." });

            return Ok( new 
            {
                authorizationToken = result.AuthorizationToken,
                urilAPI = result.ApiUrl,
            });
        }

        
    }
}