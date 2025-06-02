using Amazon.Runtime.Internal;
using APIGallery.DTO;
using APIGallery.Interfaces;
using APIGallery.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public AuthController(IUsuarioService usuarioService)
        {

            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {

            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return BadRequest(new { erro = "Usuário e senha são obrigatórios." });

            var token = _usuarioService.Autenticar(login.Email, login.Senha);

            if (token == null)
                return Unauthorized(new { erro = "Usuário ou senha incorretos" });


            return Ok(new { token });
        }
    }
}
