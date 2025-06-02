using APIGallery.Interfaces;
using APIGallery.Models;
using APIGallery.Repositorios;

namespace APIGallery.Services
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _repository;
        public ObraService(IObraRepository repository) 
        {
            _repository = repository;   
        }
        public async Task<(bool sucesso, int? QtdDownloads)> IncrementarDownload(int id)
        {
           var model = await _repository.ObterPeloId(id);

            if (model == null)
                    return (false, null);

            model.QuantidadeDownloads += 1;

            await _repository.Atualizar(model);

            return (true, model.QuantidadeDownloads);
        }
    }
}
