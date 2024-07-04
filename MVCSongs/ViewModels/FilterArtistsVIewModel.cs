using MVCSongs.Models;

namespace MVCSongs.ViewModels
{
    public class FilterArtistsVIewModel
    {
        public IList<Artist> Artist { get; set; }
        public string Ime { get; set; }
    }
}
