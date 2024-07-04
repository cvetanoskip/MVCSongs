using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSongs.Data;
using MVCSongs.Models;
using MVCSongs.ViewModels;

namespace MVCSongs.Controllers
{
    public class SongsController : Controller
    {
        private readonly MVCSongsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SongsController(MVCSongsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string Naslov,string Peac,string Zanr)
        {
            

            IQueryable<SongsGenre> songs = _context.SongsGenre.Include(x => x.Songs).Include(x=>x.Genre).AsQueryable();
            IQueryable<SongArtist> songArtists = _context.SongArtist.AsQueryable();

            IQueryable<Song> songsList = _context.Song.AsQueryable();
            IQueryable<Song> filteredSongs = _context.Song.AsQueryable();
            if (!string.IsNullOrEmpty(Naslov))
            {
                filteredSongs = songsList.Where(s => s.Title.Contains(Naslov));                
            }
            if (!string.IsNullOrEmpty(Zanr))
            {
                songs = songs.Where(s => s.Genre.GenreName == Zanr);
                filteredSongs = filteredSongs.Where(x =>  songs.Select(b=>b.SongId).Contains(x.Id));

                
            }
            if (!string.IsNullOrEmpty(Peac))
            {
                songArtists = songArtists.Where(s => s.Artist.Name.Contains(Peac));
                filteredSongs = filteredSongs.Where(x => songArtists.Select(b => b.SongId).Contains(x.Id));
                
            }

            foreach (var song in filteredSongs)
            {
                song.Rating = (float) _context.Review.Where(x => x.SongId == song.Id).Sum(x => x.Rating);
                var users = _context.Review.Where(x => x.SongId == song.Id).Select(x => x.AppUser);
                if (users.Count()!=0)
                {
                    song.Rating = song.Rating / users.Count();
                }

            }

            var songArtistGenreVM = new SongArtistGenreViewModel
            {
                Genre = new SelectList(await _context.Genre.Select(x => x.GenreName).ToListAsync()),
                //ArtistList = await _context.Artist.ToListAsync(),
                Songs = filteredSongs.Include(x => x.SongsGenre).ThenInclude(x => x.Genre).Include(x => x.SongArtist).ThenInclude(x => x.Artist).Distinct().ToList()
            };

            return View(songArtistGenreVM);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }

        // GET: Songs/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var createViewModel = new CreateEditViewModel
            {
                Song = new Song(),
                Artists = new MultiSelectList(_context.Artist.AsEnumerable(), "Id", "Name"),
                Genres = new MultiSelectList(_context.Genre.AsEnumerable(), "Id", "GenreName"),
                SelectedArtists = Enumerable.Empty<int>(),
                SelectedGenres = Enumerable.Empty<int>(),
            };

            return View(createViewModel);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(createViewModel.Song);
                await _context.SaveChangesAsync();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string uniqueFileName = string.Empty;

                if (createViewModel.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(createViewModel.File.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        createViewModel.File.CopyTo(fileStream);
                    }
                    createViewModel.ImageUrl = uniqueFileName;
                }

                if (createViewModel.SelectedArtists != null)
                {
                    foreach (var artistId in createViewModel.SelectedArtists)
                    {
                        var songArtist = new SongArtist
                        {
                            SongId = createViewModel.Song.Id,
                            ArtistId = artistId
                        };
                        _context.SongArtist.Add(songArtist);
                    }
                }
                

                if(createViewModel.SelectedGenres != null)
                {
                    foreach (var genreId in createViewModel.SelectedGenres)
                    {
                        var songArtist = new SongsGenre
                        {
                            SongId = createViewModel.Song.Id,
                            GenreId = genreId
                        };
                        _context.SongsGenre.Add(songArtist);
                    }
                }
                createViewModel.Song.AlbumCover = createViewModel.ImageUrl;

                _context.Update(createViewModel.Song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createViewModel);
        }
        [Authorize(Roles = "Admin")]
        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Song.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }
            var createEditViewModel = new CreateEditViewModel
            {
                Song = songs,
            };
            return View(createEditViewModel);
        }
       

        [Authorize(Roles = "Admin")]
        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditViewModel createEditViewModel)
        {
            if (id != createEditViewModel.Song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string uniqueFileName = string.Empty;

                    if (createEditViewModel.File != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(createEditViewModel.File.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            createEditViewModel.File.CopyTo(fileStream);
                        }
                        createEditViewModel.ImageUrl = uniqueFileName;
                    }
                    createEditViewModel.Song.AlbumCover = createEditViewModel.ImageUrl;

                    _context.Update(createEditViewModel.Song);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsExists(createEditViewModel.Song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(createEditViewModel);
        }
        [Authorize(Roles = "Admin")]
        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }
        [Authorize(Roles = "Admin")]
        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songs = await _context.Song.FindAsync(id);
            if (songs != null)
            {
                _context.Song.Remove(songs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongsExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
