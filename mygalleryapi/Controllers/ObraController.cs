using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using APIGallery.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObraController : ControllerBase
    {
        private readonly IObraRepository _obraRepository;
        private readonly IObraService _obraService;
        public ObraController(IObraRepository obraRepository, IObraService obraService)
        {
            _obraRepository = obraRepository;
            _obraService = obraService;
        }


        [HttpGet("ObterPorId")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var model = await _obraRepository.ObterPeloId(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet("ObterTodasAsObras")]
        public async Task<ActionResult<List<Obra>>> ObterTudo()
        {
            try
            {
                var obras = await _obraRepository.ObterTodos();
                return Ok(obras);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = "Erro ao buscar as obras.", detalhes = ex.Message });
            }
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> Criar([FromBody, Bind("Titulo,Descricao,URL,Tags")] Obra model)
        {
            try
            {
                var modelCriada = await _obraRepository.Criar(model);

                return Ok(modelCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Obra model)
        {
            try
            {
                var modelAtualizada = await _obraRepository.Atualizar(model);

                return Ok(modelAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var sucesso = await _obraService.IncrementarDownload(id);

                if (!sucesso.Item1)
                    return NotFound("Obra não encontrada.");
                
                return Ok(new
                {
                    Sucesso = sucesso.Item1,
                    QuantidadeDownloads = sucesso.Item2
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}