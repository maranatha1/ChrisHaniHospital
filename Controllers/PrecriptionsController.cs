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
    public class PrecriptionsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public PrecriptionsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Precriptions
        public async Task<IActionResult> Index()
        {
            var chrisHaniContext = _context.Precription.Include(p => p.Patient);
            return View(await chrisHaniContext.ToListAsync());
        }

        // GET: Precriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precription = await _context
                .Precription.Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precription == null)
            {
                return NotFound();
            }

            return View(precription);
        }

        // GET: Precriptions/Create
        public IActionResult Create()
        {
            ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "MedicationName");
            ViewBag.PatientId = _context
                .Patients.Include(a => a.User)
                .Select(p => new SelectListItem
                {
                    Text = $"{p!.User!.FirstName} {p.User.LastName}",
                    Value = p.PatientId.ToString()
                })
                .ToList();
            return View();
        }

        // POST: Precriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Precription precription)
        {
            if (ModelState.IsValid)
            {
                var patientExists = _context.Patients.Any(p =>
                    p.PatientId == precription.PatientId
                );
                if (!patientExists)
                {
                    throw new InvalidOperationException("The specified PatientId does not exist.");
                }

                var existingPrescription = _context
                    .Precription.AsNoTracking()
                    .FirstOrDefault(p => p.Id == precription.Id);

                if (existingPrescription != null)
                {
                    existingPrescription.Status = "Pending";
                    _context.Precription.Update(existingPrescription);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewData["MedicationId"] = new SelectList(
                        _context.Medication,
                        "Id",
                        "MedicationName"
                    );
                    ViewBag.PatientId = _context
                        .Patients.Include(a => a.User)
                        .Select(p => new SelectListItem
                        {
                            Text = $"{p!.User!.FirstName} {p.User.LastName}",
                            Value = p.PatientId.ToString()
                        })
                        .ToList();
                    ModelState.AddModelError("PatientId", "Please enter all required fields.");
                    return View(precription);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "MedicationName");
            ViewBag.PatientId = _context
                .Patients.Include(a => a.User)
                .Select(p => new SelectListItem
                {
                    Text = $"{p!.User!.FirstName} {p.User.LastName}",
                    Value = p.PatientId.ToString()
                })
                .ToList();
            return View(precription);
        }

        // GET: Precriptions/Edit/5

        // POST: Precriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: Precriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precription = await _context
                .Precription.Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precription == null)
            {
                return NotFound();
            }

            return View(precription);
        }

        // POST: Precriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var precription = await _context.Precription.FindAsync(id);
            if (precription != null)
            {
                _context.Precription.Remove(precription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecriptionExists(int id)
        {
            return _context.Precription.Any(e => e.Id == id);
        }
    }
}
