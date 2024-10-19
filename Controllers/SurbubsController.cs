using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Controllers
{
    public class SurbubsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public SurbubsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Surbubs
        public async Task<IActionResult> Index()
        {
            var chrisHaniContext = _context.Surbub.Include(s => s.City);
            return View(await chrisHaniContext.ToListAsync());
        }

        // GET: Surbubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surbub = await _context
                .Surbub.Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surbub == null)
            {
                return NotFound();
            }

            return View(surbub);
        }

        // GET: Surbubs/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id");
            return View();
        }

        // POST: Surbubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CityId")] Surbub surbub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surbub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", surbub.CityId);
            return View(surbub);
        }

        // GET: Surbubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surbub = await _context.Surbub.FindAsync(id);
            if (surbub == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", surbub.CityId);
            return View(surbub);
        }

        // POST: Surbubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityId")] Surbub surbub)
        {
            if (id != surbub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surbub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurbubExists(surbub.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", surbub.CityId);
            return View(surbub);
        }

        // GET: Surbubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surbub = await _context
                .Surbub.Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surbub == null)
            {
                return NotFound();
            }

            return View(surbub);
        }

        // POST: Surbubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surbub = await _context.Surbub.FindAsync(id);
            if (surbub != null)
            {
                _context.Surbub.Remove(surbub);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurbubExists(int id)
        {
            return _context.Surbub.Any(e => e.Id == id);
        }
    }
}
