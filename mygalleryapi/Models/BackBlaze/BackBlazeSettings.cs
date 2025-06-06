using System.Text.Json.Serialization;

namespace APIGallery.Models.BackBlaze
{
    public class BackBlazeSettings
    {
        public string KeyId { get; set; } = string.Empty; 
        public string MasterKey { get; set; } = string.Empty;
        public string ApiUrlBase { get; set; } = string.Empty;
        public string BucketId { get; set; } = string.Empty;
        public string BucketName { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
    }
}
