using APIGallery.Models;
using APIGallery.Repositorios;
using APIGallery.Repositorios.Interfaces;
using APIGallery.Services.Interfaces;

namespace APIGallery.Services
{
    public class WorkArtService : IWorkArtService
    {
        private readonly IWorkArtRepository _workArtRepository;
        public WorkArtService(IWorkArtRepository workArtRepository) 
        {
            _workArtRepository = workArtRepository;   
        }
        public async Task<(bool sucess, int? numberDownloads)> IncrementDownload(int id)
        {
           var model = await _workArtRepository.GetById(id);

            if (model == null)
                    return (false, null);

            model.DownloadNumber += 1;

            await _workArtRepository.Update(model);

            return (true, model.DownloadNumber);
        }
    }
}
