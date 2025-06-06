using APIGallery.Models;

namespace APIGallery.Interfaces
{
    public interface IWorkArtRepository
    {
        Task<WorkArt> Create(WorkArt model);
        Task<WorkArt> GetById(int id);
        Task<List<WorkArt>> GetAll();
        Task<WorkArt> Update(WorkArt model);
        Task<bool> Delete(int id);

    }
}
