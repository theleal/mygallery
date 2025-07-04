using APIGallery.Context;
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
using Microsoft.AspNetCore.Authorization;
using APIGallery.Services.Interfaces;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using APIGallery.DTO;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegrationBackBlazeController : ControllerBase
    {
        private readonly BackBlazeSettings _settingsBackBlaze;
        private readonly IIntegrationBackBlazeService _serviceBackBlaze;

        public IntegrationBackBlazeController(IOptions<BackBlazeSettings> settingsBackBlaze, IIntegrationBackBlazeService serviceBackBlaze)
        {
            _settingsBackBlaze = settingsBackBlaze.Value;
            _serviceBackBlaze = serviceBackBlaze;
        }

        //[Authorize]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            try
            {
                List<ResponseDTO<BackBlazeUploadResponse>> lista = new List<ResponseDTO<BackBlazeUploadResponse>>();

                foreach (var file in files)
                {
                    var fileMemory = file.OpenReadStream();

                    var result = await _serviceBackBlaze.UploadFileAsync(file.FileName, fileMemory, file.ContentType);

                    lista.Add(result);
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}