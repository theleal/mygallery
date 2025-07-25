﻿using Amazon.Runtime.Internal;
using APIGallery.DTO;
using APIGallery.Services;
using APIGallery.Services.Interfaces;
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
        public async Task<ActionResult<string>> Login([FromBody] UserDTO login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                    return BadRequest(new { erro = "Usuário e senha são obrigatórios." });

                var tokenObj = await _userService.Aunthenticate(login.Email, login.Password);

                if (tokenObj == null)
                    return Unauthorized(new { erro = "Usuário ou senha incorretos" });

                string token = tokenObj;

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
