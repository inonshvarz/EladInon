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
    public class LocationsController : Controller
    {
        private readonly PhotoContext _context;

        public LocationsController(PhotoContext context)
        {
            _context = context;
        }

        // GET: Locations
        [AllowAnonymous]
        public async Task<IActionResult> Index(string locationFilter,
                                               string locationSearchString,
                                               string locationTypeFilter,
                                               string locationTypeSearchString)
        {
            InitSearchLists();
            VerifyParameters();
            UpdateFilters();
            var filteredLocations = GetFilteredLocations();
            return View(await filteredLocations.ToListAsync());

            IQueryable<Location> GetFilteredLocations()
            {
                IQueryable<Location> locations = _context.Locations;
                locations = string.IsNullOrEmpty(locationSearchString) ? locations : locations.Where(l => l.Address == locationSearchString);
                locations = string.IsNullOrEmpty(locationTypeSearchString) ? locations : locations.Where(l => l.LocationType.ToString() == locationTypeSearchString);
                return locations;
            }

            void UpdateFilters()
            {
                ViewData["locationFilter"] = locationSearchString;
                ViewData["locationTypeFilter"] = locationTypeSearchString;
            }

            void InitSearchLists()
            {
                ViewData["Locations"] = new SelectList(_context.Locations, nameof(Location.Address), nameof(Location.Address));
                ViewData["LocationTypes"] = new SelectList(Enum.GetValues(typeof(LocationType)));
            }

            void VerifyParameters()
            {
                if (locationSearchString == null)
                    locationSearchString = locationFilter;
                if (locationTypeSearchString == null)
                    locationTypeSearchString = locationTypeFilter;
            }
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Address,LocationType,Location_X,Location_Y")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Address,LocationType,Location_X,Location_Y")] Location location)
        {
            if (id != location.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.ID))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.ID == id);
        }



        // GET: Locations/Delete/5
     //   public async Task<IActionResult> ShowOnMap(decimal Location_X, decimal Location_Y)
     //   {
        //    if (Location_X == null || Location_Y == null)
           // {
            //    return NotFound();
           // }

            //var location = await _context.Locations
            //    .FirstOrDefaultAsync(m => m.Location_X == Location_X);
            //if (location == null)
            //{
            //    return NotFound();
            //}

            //return ;//View(location);
        //}



    }
}
