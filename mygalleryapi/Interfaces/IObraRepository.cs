using APIGallery.Models;

namespace APIGallery.Interfaces
{
    public interface IObraRepository
    {
        Task<Obra> ObterPeloId(int id);
        Task<List<Obra>> ObterTodos();
        Task<Obra> Criar(Obra model);
        Task<Obra> Atualizar(Obra model);
        Task<bool> Delete(int id);
    }
}
