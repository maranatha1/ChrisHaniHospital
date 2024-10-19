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
    public class PatientVitalsController : Controller
    {
        private readonly ChrisHaniContext _context;

        public PatientVitalsController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: PatientVitals
        public async Task<IActionResult> Index()
        {
            var chrisHaniContext = _context.PatientVitals.Include(p => p.Patient);
            return View(await chrisHaniContext.ToListAsync());
        }

        // GET: PatientVitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientVitals = await _context
                .PatientVitals.Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientVitals == null)
            {
                return NotFound();
            }

            return View(patientVitals);
        }

        // GET: PatientVitals/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "AddressLine1");
            return View();
        }

        // POST: PatientVitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Id,PatientId,BodyTemparature,BloodPressure,HeartRate,RespiratoryRate,OxygenSaturation,PulseRate,DateRecorded"
            )]
                PatientVitals patientVitals
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientVitals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(
                _context.Patients,
                "Id",
                "AddressLine1",
                patientVitals.PatientId
            );
            return View(patientVitals);
        }

        // GET: PatientVitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientVitals = await _context.PatientVitals.FindAsync(id);
            if (patientVitals == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(
                _context.Patients,
                "Id",
                "AddressLine1",
                patientVitals.PatientId
            );
            return View(patientVitals);
        }

        // POST: PatientVitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "Id,PatientId,BodyTemparature,BloodPressure,HeartRate,RespiratoryRate,OxygenSaturation,PulseRate,DateRecorded"
            )]
                PatientVitals patientVitals
        )
        {
            if (id != patientVitals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientVitals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientVitalsExists(patientVitals.Id))
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
            ViewData["PatientId"] = new SelectList(
                _context.Patients,
                "Id",
                "AddressLine1",
                patientVitals.PatientId
            );
            return View(patientVitals);
        }

        // GET: PatientVitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientVitals = await _context
                .PatientVitals.Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientVitals == null)
            {
                return NotFound();
            }

            return View(patientVitals);
        }

        // POST: PatientVitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientVitals = await _context.PatientVitals.FindAsync(id);
            if (patientVitals != null)
            {
                _context.PatientVitals.Remove(patientVitals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientVitalsExists(int id)
        {
            return _context.PatientVitals.Any(e => e.Id == id);
        }
    }
}
