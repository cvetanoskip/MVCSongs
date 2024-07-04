using Microsoft.AspNetCore.Mvc.Rendering;
using MVCSongs.Models;

namespace MVCSongs.ViewModels
{
    public class SongArtistGenreViewModel
    {
        public IList<Song> Songs { get; set; }
        //public IEnumerable<Artist> ArtistList { get; set; }

        public SelectList Genre { get; set; }
        //public IList<Review> Reviews { get; set; }
        //public IList<string> Artists { get; set; }
        //public int? AvgRating { get; set; }
        public string Zanr { get; set; }
        public string Naslov { get; set; }
        public string Peac { get; set; }
    }
}
