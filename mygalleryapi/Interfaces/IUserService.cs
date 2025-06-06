using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Interfaces
{
    public interface IUserService
    {
        Task<string?> Aunthenticate(string email, string senha);
    }
}
