using System.ComponentModel.DataAnnotations;

namespace MVCSongs.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public int? Rating { get; set; }
        [StringLength(450)]
        public string AppUser { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }

    }
}
