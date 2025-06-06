namespace APIGallery.Interfaces
{
    public interface IWorkArtService
    {
        Task<(bool sucess, int? numberDownloads)> IncrementarDownload(int id);
    }
}
