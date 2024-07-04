using System.ComponentModel.DataAnnotations;

namespace MVCSongs.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string GenreName { get; set; }
        public ICollection<SongsGenre>? SongsGenre { get; set; }
    }
}
