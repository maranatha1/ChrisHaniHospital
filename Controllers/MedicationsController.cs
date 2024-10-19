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
    public class MedicationsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public MedicationsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Medications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medication.ToListAsync());
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication.FirstOrDefaultAsync(m => m.Id == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medication);
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return View(medication);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,MedicationName,Schedule,Dosage")] Medication medication
        )
        {
            if (id != medication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.Id))
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
            return View(medication);
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication.FirstOrDefaultAsync(m => m.Id == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medication = await _context.Medication.FindAsync(id);
            if (medication != null)
            {
                _context.Medication.Remove(medication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationExists(int id)
        {
            return _context.Medication.Any(e => e.Id == id);
        }
    }
}
