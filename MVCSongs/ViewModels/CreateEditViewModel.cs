using Microsoft.AspNetCore.Mvc.Rendering;
using MVCSongs.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCSongs.ViewModels
{
    public class CreateEditViewModel
    {
        //public string Title { get; set; }
        //public int? Year { get; set; }
        //public int? Listeners { get; set; }
        //[StringLength(int.MaxValue)]
        //public string? Description { get; set; }
        //[StringLength(50)]
        //public string? Publisher { get; set; }
        public Song? Song { get; set; }
        public IEnumerable<SelectListItem>? Artists { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<int>? SelectedArtists { get; set; }
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; }


    }
}
