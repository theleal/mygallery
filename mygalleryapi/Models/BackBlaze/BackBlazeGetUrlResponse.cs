using System.Text.Json.Serialization;

namespace APIGallery.Models
{
    public class BackBlazeGetUrlResponse
    {
        public string AuthorizationToken { get; set; } = string.Empty;
        public string UploadUrl { get; set; } = string.Empty;
    }
}
