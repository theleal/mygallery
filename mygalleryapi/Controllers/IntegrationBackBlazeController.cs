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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var fileMemory = file.OpenReadStream();

                var a = await _serviceBackBlaze.UploadFileAsync(file.FileName, fileMemory, file.ContentType);
                return Content(a.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}