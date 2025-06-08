using APIGallery.Context;
using APIGallery.Models;
using APIGallery.Repositorios.Interfaces;
using APIGallery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkArtController : ControllerBase
    {
        private readonly IWorkArtRepository _workArtRepository;
        private readonly IWorkArtService _workArtService;
        public WorkArtController(IWorkArtRepository workArtRepository, IWorkArtService workArtService)
        {
            _workArtRepository = workArtRepository;
            _workArtService = workArtService;
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _workArtRepository.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet("GetAllWorkArt")]
        public async Task<ActionResult<List<WorkArt>>> GetAll()
        {
            try
            {
                var obras = await _workArtRepository.GetAll();
                return Ok(obras);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = "Erro ao buscar as obras.", ErrorMessage = ex.Message });
            }
        }

        [HttpPost("CreateWorkArt")]
        public async Task<IActionResult> Criar([FromBody, Bind("Tittle,Description,URL,Tags")] WorkArt model)
        {
            try
            {
                var cretedModel = await _workArtRepository.Create(model);

                return Ok(cretedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateWorkArt")]
        public async Task<IActionResult> Atualizar([FromBody] WorkArt model)
        {
            try
            {
                var updatedModel = await _workArtRepository.Update(model);

                return Ok(updatedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("DownloadWorkArt")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var sucess = await _workArtService.IncrementDownload(id);

                if (!sucess.Item1)
                    return NotFound("WorkArt não encontrada.");
                
                return Ok(new
                {
                    Sucesso = sucess.Item1,
                    QuantidadeDownloads = sucess.Item2
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}