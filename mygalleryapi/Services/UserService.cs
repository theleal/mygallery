using APIGallery.Interfaces;
using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Services
{
    public class UserService : IUserService
    {
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository repository, TokenService tokenService)
        {
            _userRepository = _userRepository;
            _tokenService = tokenService;
        }

        public async Task<string?> Aunthenticate(string email, string senha)
        {
            var model = await _userRepository.GetByEmail(email);


            if (model == null || !BCrypt.Net.BCrypt.Verify(senha, model.PasswordHash))
                return null;

            return _tokenService.GenerateToken(model.Name);

        }
    }
}
