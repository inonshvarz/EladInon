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
    public class PackgesController : Controller
    {
        private readonly PhotoContext _context;

        public PackgesController(PhotoContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Packges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Packges.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Packges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packge = await _context.Packges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (packge == null)
            {
                return NotFound();
            }

            return View(packge);
        }

        // GET: Packges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,AlbumType,Price")] Packge packge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packge);
        }

        // GET: Packges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packge = await _context.Packges.FindAsync(id);
            if (packge == null)
            {
                return NotFound();
            }
            return View(packge);
        }

        // POST: Packges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Name,Description,AlbumType,Price")] Packge packge)
        {
            if (id != packge.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackgeExists(packge.ID))
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
            return View(packge);
        }

        // GET: Packges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packge = await _context.Packges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (packge == null)
            {
                return NotFound();
            }

            return View(packge);
        }

        // POST: Packges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var packge = await _context.Packges.FindAsync(id);
            _context.Packges.Remove(packge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackgeExists(int? id)
        {
            return _context.Packges.Any(e => e.ID == id);
        }
    }
}
