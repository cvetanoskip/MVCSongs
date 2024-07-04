using System.ComponentModel.DataAnnotations;

namespace MVCSongs.Models
{
    public class Song
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public int? Year { get; set; }
        
        [StringLength (int.MaxValue)]
        public string? Description { get; set; }
        [StringLength(50)]
        public string? Publisher { get; set; }
        [StringLength(int.MaxValue)]
        public string? AlbumCover { get; set; }
        [StringLength(int.MaxValue)]
        public string? SongUrl { get;  set; }
        //public int ArtistId { get; set; }
        public float Rating { get; set; }
        public ICollection<SongArtist>? SongArtist { get; set; }
        public ICollection<SongsGenre>? SongsGenre { get; set;}
    }
}
