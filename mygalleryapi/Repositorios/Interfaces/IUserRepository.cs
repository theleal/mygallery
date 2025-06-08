using APIGallery.Models;

namespace APIGallery.Repositorios.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
    }
}
