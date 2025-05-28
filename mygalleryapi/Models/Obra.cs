using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGallery.Models
{
    public class Obra
    {
        [Key]
        [Column(TypeName = "integer")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")] 
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "varchar(1000)")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataUpload { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string URL { get; set; } = string.Empty;

        [Column(TypeName = "integer")]
        public int QuantidadeDownloads { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Tags { get; set; }
    }
}