using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TokenService _tokenService;
        private readonly IUsuarioRepository _repository;


        public UsuarioService(IUsuarioRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<string?> Autenticar(string email, string senha)
        {
            var model = await _repository.ObterPeloEmail(email);


            if (model == null || !BCrypt.Net.BCrypt.Verify(senha, model.SenhaHash))
                return null;

            return _tokenService.GenerateToken(model.Nome);

        }
    }
}
