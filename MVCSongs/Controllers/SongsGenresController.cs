using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSongs.Data;
using MVCSongs.Models;

namespace MVCSongs.Controllers
{
    public class SongsGenresController : Controller
    {
        private readonly MVCSongsContext _context;

        public SongsGenresController(MVCSongsContext context)
        {
            _context = context;
        }

        // GET: SongsGenres
        public async Task<IActionResult> Index()
        {
            var mVCSongsContext = _context.SongsGenre.Include(s => s.Genre);
            return View(await mVCSongsContext.ToListAsync());
        }

        // GET: SongsGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsGenre = await _context.SongsGenre
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songsGenre == null)
            {
                return NotFound();
            }

            return View(songsGenre);
        }

        // GET: SongsGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id");
            return View();
        }

        // POST: SongsGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,GenreId")] SongsGenre songsGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songsGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", songsGenre.GenreId);
            return View(songsGenre);
        }

        // GET: SongsGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsGenre = await _context.SongsGenre.FindAsync(id);
            if (songsGenre == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", songsGenre.GenreId);
            return View(songsGenre);
        }

        // POST: SongsGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,GenreId")] SongsGenre songsGenre)
        {
            if (id != songsGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songsGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsGenreExists(songsGenre.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", songsGenre.GenreId);
            return View(songsGenre);
        }

        // GET: SongsGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songsGenre = await _context.SongsGenre
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songsGenre == null)
            {
                return NotFound();
            }

            return View(songsGenre);
        }

        // POST: SongsGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songsGenre = await _context.SongsGenre.FindAsync(id);
            if (songsGenre != null)
            {
                _context.SongsGenre.Remove(songsGenre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongsGenreExists(int id)
        {
            return _context.SongsGenre.Any(e => e.Id == id);
        }
    }
}
