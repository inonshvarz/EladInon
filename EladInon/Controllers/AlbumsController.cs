using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EladInon.Data;
using EladInon.Models;
using Microsoft.AspNetCore.Authorization;

namespace EladInon.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private readonly PhotoContext _context;

        public AlbumsController(PhotoContext context)
        {
            _context = context;
        }

        // GET: Albums
        [AllowAnonymous]
        public async Task<IActionResult> Index(string albumFilter,
                                               string albumSearchString,
                                               string locationFilter,
                                               string locationSearchString,
                                               string albumTypeFilter,
                                               string albumTypeSearchString,
                                               string locationTypeFilter,
                                               string locationTypeSearchString)
        {
            InitSearchLists();
            VerifyParameters();
            UpdateFilters();
            var filteredalbums = GetFilteredalbums();
            return View(await filteredalbums.ToListAsync());

            IQueryable<Album> GetFilteredalbums()
            {
                IQueryable<Album> albums = _context.Albums.Include(a => a.Pictures).Include(a => a.AlbumLocation);
                albums = string.IsNullOrEmpty(albumSearchString) ? albums : albums.Where(a => a.Name == albumSearchString);
                albums = string.IsNullOrEmpty(locationSearchString) ? albums : albums.Where(a => a.AlbumLocation.Address == locationSearchString);
                albums = string.IsNullOrEmpty(locationTypeSearchString) ? albums : albums.Where(a => a.AlbumLocation.LocationType.ToString() == locationTypeSearchString);
                albums = string.IsNullOrEmpty(albumTypeSearchString) ? albums : albums.Where(a => a.AlbumType.ToString() == albumTypeSearchString);
                return albums;
            }

            void UpdateFilters()
            {
                ViewData["locationFilter"] = locationSearchString;
                ViewData["albumFilter"] = albumSearchString;
                ViewData["locationTypeFilter"] = locationTypeSearchString;
                ViewData["albumTypeFilter"] = albumTypeSearchString;
            }

            void InitSearchLists()
            {
                ViewData["Locations"] = new SelectList(_context.Locations, nameof(Location.Address), nameof(Location.Address));
                ViewData["Albums"] = new SelectList(_context.Albums, nameof(Album.Name), nameof(Album.Name));
                ViewData["AlbumTypes"] = new SelectList(Enum.GetValues(typeof(AlbumType)));
                ViewData["LocationTypes"] = new SelectList(Enum.GetValues(typeof(LocationType)));
            }

            void VerifyParameters()
            {
                if (albumSearchString == null)
                    albumSearchString = albumFilter;
                if (locationSearchString == null)
                    locationSearchString = locationFilter;
                if (albumTypeSearchString == null)
                    albumTypeSearchString = albumTypeFilter;
                if (locationTypeSearchString == null)
                    locationTypeSearchString = locationTypeFilter;
            }
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["Locations"] = new SelectList(_context.Locations, nameof(Location.ID), nameof(Location.Address));
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Time,AlbumType,Name,LocationID")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Time,AlbumType")] Album album)
        {
            if (id != album.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.ID))
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
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int? id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}
