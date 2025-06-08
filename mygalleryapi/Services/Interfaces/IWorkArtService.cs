namespace APIGallery.Services.Interfaces
{
    public interface IWorkArtService
    {
        Task<(bool sucess, int? numberDownloads)> IncrementDownload(int id);
    }
}
