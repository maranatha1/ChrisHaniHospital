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
    public class ConditionsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public ConditionsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Conditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conditions.ToListAsync());
        }

        // GET: Conditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditions = await _context.Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conditions == null)
            {
                return NotFound();
            }

            return View(conditions);
        }

        // GET: Conditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConditionName,Description")] Conditions conditions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conditions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conditions);
        }

        // GET: Conditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditions = await _context.Conditions.FindAsync(id);
            if (conditions == null)
            {
                return NotFound();
            }
            return View(conditions);
        }

        // POST: Conditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConditionName,Description")] Conditions conditions)
        {
            if (id != conditions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conditions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConditionsExists(conditions.Id))
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
            return View(conditions);
        }

        // GET: Conditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditions = await _context.Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conditions == null)
            {
                return NotFound();
            }

            return View(conditions);
        }

        // POST: Conditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conditions = await _context.Conditions.FindAsync(id);
            if (conditions != null)
            {
                _context.Conditions.Remove(conditions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConditionsExists(int id)
        {
            return _context.Conditions.Any(e => e.Id == id);
        }
    }
}
