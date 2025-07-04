using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGallery.Models
{
    public class WorkArt
    {
        [Key]
        [Column(TypeName = "integer")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")] 
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateUpload { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string URL { get; set; } = string.Empty;

        [Column(TypeName = "integer")]
        public int DownloadNumber { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string idFileBackBlaze { get; set; } = string.Empty;

        [Column(TypeName = "varchar(5)")]
        public string ISO { get; set; } = string.Empty;

        [Column(TypeName = "varchar(10)")]
        public string Speed { get; set; } = string.Empty;

        [Column(TypeName = "varchar(10)")]
        public string Aperture { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Tags { get; set; } = string.Empty;
    }
}