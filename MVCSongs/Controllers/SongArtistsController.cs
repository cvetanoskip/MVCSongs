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
    public class SongArtistsController : Controller
    {
        private readonly MVCSongsContext _context;

        public SongArtistsController(MVCSongsContext context)
        {
            _context = context;
        }

        // GET: SongArtists
        public async Task<IActionResult> Index()
        {
            var mVCSongsContext = _context.SongArtist.Include(s => s.Artist).Include(s => s.Song);
            return View(await mVCSongsContext.ToListAsync());
        }

        // GET: SongArtists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songArtist = await _context.SongArtist
                .Include(s => s.Artist)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songArtist == null)
            {
                return NotFound();
            }

            return View(songArtist);
        }

        // GET: SongArtists/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "Id", "Id");
            return View();
        }

        // POST: SongArtists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,ArtistId")] SongArtist songArtist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songArtist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", songArtist.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "Id", "Id", songArtist.SongId);
            return View(songArtist);
        }

        // GET: SongArtists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songArtist = await _context.SongArtist.FindAsync(id);
            if (songArtist == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", songArtist.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "Id", "Id", songArtist.SongId);
            return View(songArtist);
        }

        // POST: SongArtists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,ArtistId")] SongArtist songArtist)
        {
            if (id != songArtist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songArtist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongArtistExists(songArtist.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", songArtist.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "Id", "Id", songArtist.SongId);
            return View(songArtist);
        }

        // GET: SongArtists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songArtist = await _context.SongArtist
                .Include(s => s.Artist)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songArtist == null)
            {
                return NotFound();
            }

            return View(songArtist);
        }

        // POST: SongArtists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songArtist = await _context.SongArtist.FindAsync(id);
            if (songArtist != null)
            {
                _context.SongArtist.Remove(songArtist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongArtistExists(int id)
        {
            return _context.SongArtist.Any(e => e.Id == id);
        }
    }
}
