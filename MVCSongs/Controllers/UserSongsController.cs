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
    public class UserSongsController : Controller
    {
        private readonly MVCSongsContext _context;

        public UserSongsController(MVCSongsContext context)
        {
            _context = context;
        }

        // GET: UserSongs
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserSongs.ToListAsync());
        }

        // GET: UserSongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSongs = await _context.UserSongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSongs == null)
            {
                return NotFound();
            }

            return View(userSongs);
        }

        // GET: UserSongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUser,SongId")] UserSongs userSongs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSongs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userSongs);
        }

        // GET: UserSongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSongs = await _context.UserSongs.FindAsync(id);
            if (userSongs == null)
            {
                return NotFound();
            }
            return View(userSongs);
        }

        // POST: UserSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUser,SongId")] UserSongs userSongs)
        {
            if (id != userSongs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSongs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSongsExists(userSongs.Id))
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
            return View(userSongs);
        }

        // GET: UserSongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSongs = await _context.UserSongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSongs == null)
            {
                return NotFound();
            }

            return View(userSongs);
        }

        // POST: UserSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSongs = await _context.UserSongs.FindAsync(id);
            if (userSongs != null)
            {
                _context.UserSongs.Remove(userSongs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSongsExists(int id)
        {
            return _context.UserSongs.Any(e => e.Id == id);
        }
    }
}
