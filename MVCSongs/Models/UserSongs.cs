using System.ComponentModel.DataAnnotations;

namespace MVCSongs.Models
{
    public class UserSongs
    {
        public int Id { get; set; }
        [StringLength(450)]
        public string AppUser { get; set; }
        public int SongId { get; set; }
    }
}
