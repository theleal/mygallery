using APIGallery.Models;

namespace APIGallery.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
    }
}
