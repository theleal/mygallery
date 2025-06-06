using System.Text.Json.Serialization;

namespace APIGallery.Models
{
    public class BackBlazeUploadResponse
    {
        public string FileId { get; set; } = string.Empty;
        public string PublicUrl{ get; set; } = string.Empty;
 
    }
}
