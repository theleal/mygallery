using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObraController : ControllerBase
    {
        private readonly IObraRepository _obraRepository;
        public ObraController(IObraRepository obraRepository)
        {
            _obraRepository = obraRepository;
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
        public async Task<IActionResult> Criar([FromBody] Obra model)
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

        [HttpPost("Atualizar")]
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



    }
}