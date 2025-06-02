using APIGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGallery.Interfaces
{
    public interface IUsuarioService
    {
        Task<string?> Autenticar(string email, string senha);
    }
}
