using System.ComponentModel.DataAnnotations;

namespace MVCSongs.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        [StringLength(50)]
        public string? Nationality { get; set; }
        [StringLength(50)]
        public string? Gender { get; set; }
        public ICollection<SongArtist>? SongArtist { get; set; }
    }
}
