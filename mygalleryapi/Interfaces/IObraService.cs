namespace APIGallery.Interfaces
{
    public interface IObraService
    {
        Task<(bool sucesso, int? QtdDownloads)> IncrementarDownload(int id);
    }
}
