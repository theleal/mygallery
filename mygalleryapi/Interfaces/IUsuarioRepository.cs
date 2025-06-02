using APIGallery.Models;

namespace APIGallery.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPeloEmail(string email);
    }
}
