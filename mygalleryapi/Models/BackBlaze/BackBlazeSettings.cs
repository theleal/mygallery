using System.Text.Json.Serialization;

namespace APIGallery.Models.BackBlaze
{
    public class BackBlazeSettings
    {
        public string KeyId { get; set; }
        public string MasterKey { get; set; }
        public string ApiUrl { get; set; }
        public string BucketId { get; set; }
    }
}
