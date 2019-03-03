using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EladInon.Data;
using EladInon.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace EladInon.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {
        private IHostingEnvironment env;
        private readonly PhotoContext _context;

        public PicturesController(PhotoContext context, IHostingEnvironment env)
        {
            this.env = env;
            _context = context;
        }

        // GET: Pictures
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
            var filteredPictures = GetFilteredPictures();
            return View(await filteredPictures.ToListAsync());

            IQueryable<Picture> GetFilteredPictures()
            {
                IQueryable<Picture> pictures = _context.Pictures.Include(p => p.ContainingAlbum).ThenInclude(a => a.AlbumLocation);
                pictures = string.IsNullOrEmpty(albumSearchString) ? pictures : pictures.Where(p => p.ContainingAlbum.Name == albumSearchString);
                pictures = string.IsNullOrEmpty(locationSearchString) ? pictures : pictures.Where(p => p.ContainingAlbum.AlbumLocation.Address == locationSearchString);
                pictures = string.IsNullOrEmpty(locationTypeSearchString) ? pictures : pictures.Where(p => p.ContainingAlbum.AlbumLocation.LocationType.ToString() == locationTypeSearchString);
                pictures = string.IsNullOrEmpty(albumTypeSearchString) ? pictures : pictures.Where(p => p.ContainingAlbum.AlbumType.ToString() == albumTypeSearchString);
                return pictures;
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

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.ID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

       
        // GET: Pictures/Create
        public IActionResult Create()
        {
            ViewData["Albums"] = new SelectList(_context.Albums, nameof(Album.ID), nameof(Album.Name));
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Image,AlbumID")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                if (picture.Image != null)
                    try
                    {
                        picture.Path = Path.Combine("Pictures",
                                                    Path.GetFileName(picture.Image.FileName));
                        var fullPath = Path.Combine(env.WebRootPath, picture.Path);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                            await picture.Image.CopyToAsync(stream);
                        ViewBag.Message = "File uploaded successfully";
                        _context.Add(picture);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Path")] Picture picture)
        {
            if (id != picture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(picture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.ID))
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
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.ID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            var fullPath = Path.Combine(env.WebRootPath, picture.Path);
            System.IO.File.Delete(fullPath);
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PictureExists(int? id)
        {
            return _context.Pictures.Any(e => e.ID == id);
        }
    }
}
