using System.Text.Json.Serialization;

namespace APIGallery.Models
{
    public class BackBlazeAuthResponse
    {
        public string AuthorizationToken { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty;
    }
}
