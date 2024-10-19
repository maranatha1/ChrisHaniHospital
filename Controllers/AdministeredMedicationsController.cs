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
    public class AdministeredMedicationsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public AdministeredMedicationsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: AdministeredMedications
        public async Task<IActionResult> Index(int PresciptionId)
        {
            var chrisHaniContext = await _context
                .administeredMedications.Include(a => a.Medication)
                .Include(a => a.Prescription)
                .Where(a => a.PrescriptionId == PresciptionId)
                .ToListAsync();
            var precription = _context
                .Precription.Where(a => a.Id == PresciptionId)
                .Include(a => a.Patient)
                .ThenInclude(a => a.User)
                .FirstOrDefault();
            var PatientPrescription = _context
                .Precription.Where(a => a.Id == PresciptionId && a.Status != "Default")
                .Select(a => new PrescriptionListVM
                {
                    Precription = a,
                    Medications = _context
                        .PrescriptionMedications.Where(pm => pm.PrescriptionId == a.Id)
                        .Include(pm => pm.Medication)
                        .OrderBy(a => a.Medication.MedicationName)
                        .ToList()
                });
            var viewModel = new AdministeredVM();
            viewModel.AdministeredMedications = chrisHaniContext;
            viewModel.Precription = precription!;
            viewModel.PrescriptionListVM = PatientPrescription.FirstOrDefault()!;

            return View(viewModel);
        }

        // GET: AdministeredMedications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administeredMedication = await _context
                .administeredMedications.Include(a => a.Medication)
                .Include(a => a.Prescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administeredMedication == null)
            {
                return NotFound();
            }

            return View(administeredMedication);
        }

        // GET: AdministeredMedications/Create
        public IActionResult Create()
        {
            ViewData["MedicationId"] = new SelectList(_context.Medication, "Id", "Dosage");
            ViewData["PrescriptionId"] = new SelectList(_context.Precription, "Id", "Id");
            return View();
        }

        // POST: AdministeredMedications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MedicationId,Quantity,Notes,AdministeredDate,PrescriptionId")]
                AdministeredMedication administeredMedication
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(administeredMedication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicationId"] = new SelectList(
                _context.Medication,
                "Id",
                "Dosage",
                administeredMedication.MedicationId
            );
            ViewData["PrescriptionId"] = new SelectList(
                _context.Precription,
                "Id",
                "Id",
                administeredMedication.PrescriptionId
            );
            return View(administeredMedication);
        }

        // GET: AdministeredMedications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administeredMedication = await _context.administeredMedications.FindAsync(id);
            if (administeredMedication == null)
            {
                return NotFound();
            }
            ViewData["MedicationId"] = new SelectList(
                _context.Medication,
                "Id",
                "Dosage",
                administeredMedication.MedicationId
            );
            ViewData["PrescriptionId"] = new SelectList(
                _context.Precription,
                "Id",
                "Id",
                administeredMedication.PrescriptionId
            );
            return View(administeredMedication);
        }

        // POST: AdministeredMedications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,MedicationId,Quantity,Notes,AdministeredDate,PrescriptionId")]
                AdministeredMedication administeredMedication
        )
        {
            if (id != administeredMedication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administeredMedication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministeredMedicationExists(administeredMedication.Id))
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
            ViewData["MedicationId"] = new SelectList(
                _context.Medication,
                "Id",
                "Dosage",
                administeredMedication.MedicationId
            );
            ViewData["PrescriptionId"] = new SelectList(
                _context.Precription,
                "Id",
                "Id",
                administeredMedication.PrescriptionId
            );
            return View(administeredMedication);
        }

        // GET: AdministeredMedications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administeredMedication = await _context
                .administeredMedications.Include(a => a.Medication)
                .Include(a => a.Prescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administeredMedication == null)
            {
                return NotFound();
            }

            return View(administeredMedication);
        }

        // POST: AdministeredMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administeredMedication = await _context.administeredMedications.FindAsync(id);
            if (administeredMedication != null)
            {
                _context.administeredMedications.Remove(administeredMedication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministeredMedicationExists(int id)
        {
            return _context.administeredMedications.Any(e => e.Id == id);
        }
    }
}
