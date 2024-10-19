using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;

namespace ChrisHaniHospital.Controllers
{
    public class AllergiesController : Controller
    {
        private readonly ChrisHaniContext _context;

        public AllergiesController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Allergies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Allergies.ToListAsync());
        }

        // GET: Allergies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allergies == null)
            {
                return NotFound();
            }

            return View(allergies);
        }

        // GET: Allergies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Allergies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AllergyName,Description")] Allergies allergies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allergies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allergies);
        }

        // GET: Allergies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies.FindAsync(id);
            if (allergies == null)
            {
                return NotFound();
            }
            return View(allergies);
        }

        // POST: Allergies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AllergyName,Description")] Allergies allergies)
        {
            if (id != allergies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allergies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllergiesExists(allergies.Id))
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
            return View(allergies);
        }

        // GET: Allergies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allergies == null)
            {
                return NotFound();
            }

            return View(allergies);
        }

        // POST: Allergies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allergies = await _context.Allergies.FindAsync(id);
            if (allergies != null)
            {
                _context.Allergies.Remove(allergies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllergiesExists(int id)
        {
            return _context.Allergies.Any(e => e.Id == id);
        }
    }
}
