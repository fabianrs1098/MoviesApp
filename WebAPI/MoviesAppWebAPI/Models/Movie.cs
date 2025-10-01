using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAppWebAPI.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime ReleaseYear { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public TimeSpan Duration { get; set; }

        [ForeignKey("Director")]
        public string FKDirector { get; set; }
    }
}
