namespace MVCSongs.Models
{
    public class SongsGenre
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song? Songs { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
