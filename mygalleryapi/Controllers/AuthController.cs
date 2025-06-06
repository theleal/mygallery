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
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {

            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                    return BadRequest(new { erro = "Usuário e senha são obrigatórios." });

                var tokenObj = _userService.Aunthenticate(login.Email, login.Password);

                if (tokenObj == null)
                    return Unauthorized(new { erro = "Usuário ou senha incorretos" });

                string token = tokenObj.Result;

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
