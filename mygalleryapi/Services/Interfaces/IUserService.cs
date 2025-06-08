using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> Aunthenticate(string email, string senha);
    }
}
