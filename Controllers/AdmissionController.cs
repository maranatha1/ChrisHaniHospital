using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisHaniHospital.Areas.Identity.Data;
using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Controllers
{
    public class AdmissionController : Controller
    {
        private readonly ChrisHaniContext _context;

        public AdmissionController(ChrisHaniContext context)
        {
            _context = context;
        }

        // GET: Admission
        public async Task<IActionResult> Index()
        {
            var AdmmitedList = _context
                .AdmittedPatients.Include(a => a.Patient)
                .ThenInclude(a => a.User)
                .Include(a => a.Suburb)
                .Include(a => a.Surgeon)
                .ThenInclude(a => a.User)
                .Include(a => a.Ward)
                .OrderBy(a => a.DateAdmitted)
                .ToList();
            return View(AdmmitedList);
        }

        // GET: Admission/Details/5


        // GET: Admission/Create
        public IActionResult Create(int? PatientID)
        {
            ViewBag.AlergyID = _context.Allergies.ToList().OrderBy(a => a.AllergyName);
            ViewData["ConditionID"] = new SelectList(_context.Conditions, "Id", "ConditionName");
            ViewData["MedicationID"] = new SelectList(
                _context.Medication,
                "Id",
                "MedicationName"
            ).OrderBy(a => a.Text);
            ViewData["SuburbId"] = _context
                .Surbub.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList()
                .OrderBy(a => a.Text);
            ViewData["SurgeonID"] = _context
                .Surgeons.Include(a => a.User)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.User.FirstName + " " + a.User.LastName
                })
                .ToList()
                .OrderBy(a => a.Text);

            ViewData["WardId"] = new SelectList(_context.Set<Ward>(), "Id", "Name").OrderBy(a =>
                a.Text
            );
            if (PatientID != null)
            {
                var Patient = _context
                    .Patients.Where(a => a.PatientId == PatientID)
                    .Include(a => a.User)
                    .FirstOrDefault();
                if (Patient != null && Patient.User != null)
                {
                    AdmittedVM admittedVM = new AdmittedVM();
                    admittedVM.IdentityNumber = Patient.User.IdentityNumber;
                    admittedVM.PatientId = (int)PatientID;
                    admittedVM.FirstName = Patient.User.FirstName;
                    admittedVM.LastName = Patient.User.LastName;
                    admittedVM.Email = Patient.User.Email;
                    admittedVM.PhoneNumber = Patient.User.PhoneNumber;
                    ViewBag.Date = DateTime.Now.ToString("dd/MMM/yyyy HH:mm");

                    return View(admittedVM);
                }
            }
            ViewBag.Date = DateTime.Now.ToString("dd/MMM/yyyy HH:mm");
            return View();
        }

        // POST: Admission/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdmittedVM admittedVM)
        {
            if (ModelState.IsValid)
            {
                //Update the identity user
                var PatientID = _context.Patients.Find(admittedVM.PatientId);
                if (PatientID != null)
                {
                    var user = _context.Users.Find(PatientID.UserId);
                    if (user != null)
                    {
                        user.PhoneNumber = admittedVM.PhoneNumber;
                        user.IdentityNumber = admittedVM.IdentityNumber;

                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                }
                PatientVitals patientVitals = new PatientVitals();
                patientVitals.BodyTemparature = admittedVM.BodyTemparature;
                patientVitals.BloodPressure = admittedVM.BloodPressure;
                patientVitals.HeartRate = admittedVM.HeartRate;
                patientVitals.RespiratoryRate = admittedVM.RespiratoryRate;
                patientVitals.OxygenSaturation = admittedVM.OxygenSaturation;
                patientVitals.PulseRate = admittedVM.PulseRate;
                patientVitals.DateRecorded = DateTime.Now;
                patientVitals.PatientId = admittedVM.PatientId;
                await _context.PatientVitals.AddAsync(patientVitals);
                await _context.SaveChangesAsync();

                AdmittedPatient patient = new AdmittedPatient();
                patient.WardId = admittedVM.WardId;
                patient.DateAdmitted = DateTime.Now;
                patient.SurgeonID = admittedVM.SurgeonID;
                patient.AddressLine1 = admittedVM.AddressLine1;
                patient.AddressLine2 = admittedVM.AddressLine2;
                patient.SuburbId = admittedVM.SuburbId;
                patient.Bed = admittedVM.Bed;
                patient.PatinetId = admittedVM.PatientId;
                await _context.AdmittedPatients.AddAsync(patient);
                await _context.SaveChangesAsync();

                PatientID.AddressLine1 = admittedVM.AddressLine1;
                PatientID.AddressLine2 = admittedVM.AddressLine2;
                PatientID.SuburbId = admittedVM.SuburbId;
                _context.Patients.Update(PatientID);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Patient Admitted Successfully";
                return RedirectToAction(nameof(Index));
            }
            if (admittedVM.PatientId != null)
            {
                var Patient = _context
                    .Patients.Where(a => a.PatientId == admittedVM.PatientId)
                    .Include(a => a.User)
                    .FirstOrDefault();
                if (Patient != null && Patient.User != null)
                {
                    admittedVM.PatientId = Patient.PatientId;
                    admittedVM.FirstName = Patient.User.FirstName;
                    admittedVM.LastName = Patient.User.LastName;
                    admittedVM.Email = Patient.User.Email;
                    admittedVM.PhoneNumber = Patient.User.PhoneNumber;
                    ViewBag.Date = DateTime.Now.ToString("dd/MMM/yyyy HH:mm");
                }
            }
            ViewBag.AlergyID = _context.Allergies.ToList().OrderBy(a => a.AllergyName);
            ViewData["ConditionID"] = new SelectList(
                _context.Conditions,
                "Id",
                "ConditionName"
            ).OrderBy(a => a.Text);
            ViewData["MedicationID"] = new SelectList(
                _context.Medication,
                "Id",
                "MedicationName"
            ).OrderBy(a => a.Text);
            ViewData["SuburbId"] = _context
                .Surbub.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList();
            ViewData["SurgeonID"] = _context
                .Surgeons.Include(a => a.User)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.User.FirstName + " " + a.User.LastName
                })
                .ToList();

            ViewData["WardId"] = new SelectList(_context.Set<Ward>(), "Id", "Name");
            ViewBag.Date = DateTime.Now.ToString("dd/MMM/yyyy HH:mm");
            return View(admittedVM);
        }

        // GET: Admission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admittedVM = await _context.AdmittedPatients.FindAsync(id);
            if (admittedVM == null)
            {
                return NotFound();
            }
            //ViewData["AlergyID"] = new SelectList(
            //    _context.Allergies,
            //    "Id",
            //    "AllergyName",
            //    admittedVM.AlergyID
            //);
            //ViewData["ConditionID"] = new SelectList(
            //    _context.Conditions,
            //    "Id",
            //    "ConditionName",
            //    admittedVM.ConditionID
            //);
            //ViewData["MedicationID"] = new SelectList(
            //    _context.Medication,
            //    "Id",
            //    "Dosage",
            //    admittedVM.MedicationID
            //);
            ViewData["PatientId"] = new SelectList(
                _context.Patients,
                "Id",
                "AddressLine1",
                admittedVM.PatinetId
            );
            ViewData["SuburbId"] = new SelectList(_context.Surbub, "Id", "Id", admittedVM.SuburbId);
            ViewData["SurgeonID"] = new SelectList(
                _context.Surgeons,
                "Id",
                "Registration_Number",
                admittedVM.SurgeonID
            );
            ViewData["WardId"] = new SelectList(
                _context.Set<Ward>(),
                "Id",
                "Name",
                admittedVM.WardId
            );

            return View(admittedVM);
        }

        // POST: Admission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "Id,WardId,DateAdmitted,SurgeonID,AddressLine1,AddressLine2,SuburbId,AlergyID,ConditionID,MedicationID,PatientId,BodyTemparature,BloodPressure,HeartRate,RespiratoryRate,OxygenSaturation,PulseRate,DateRecorded"
            )]
                AdmittedVM admittedVM
        )
        {
            if (id != admittedVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admittedVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmittedVMExists(admittedVM.Id))
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
            ViewData["AlergyID"] = new SelectList(
                _context.Allergies,
                "Id",
                "AllergyName",
                admittedVM.AlergyID
            );
            ViewData["ConditionID"] = new SelectList(
                _context.Conditions,
                "Id",
                "ConditionName",
                admittedVM.ConditionID
            );
            ViewData["MedicationID"] = new SelectList(
                _context.Medication,
                "Id",
                "Dosage",
                admittedVM.MedicationID
            );
            ViewData["PatientId"] = new SelectList(
                _context.Patients,
                "Id",
                "AddressLine1",
                admittedVM.PatientId
            );
            ViewData["SuburbId"] = new SelectList(_context.Surbub, "Id", "Id", admittedVM.SuburbId);
            ViewData["SurgeonID"] = new SelectList(
                _context.Surgeons,
                "Id",
                "Registration_Number",
                admittedVM.SurgeonID
            );
            ViewData["WardId"] = new SelectList(
                _context.Set<Ward>(),
                "Id",
                "Name",
                admittedVM.WardId
            );
            return View(admittedVM);
        }

        // GET: Admission/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var admittedVM = await _context
        //        .AdmittedVM.Include(a => a.Allergies)
        //        .Include(a => a.Conditions)
        //        .Include(a => a.Medication)
        //        .Include(a => a.Patient)
        //        .Include(a => a.Suburb)
        //        .Include(a => a.Surgeon)
        //        .Include(a => a.Ward)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (admittedVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(admittedVM);
        //}

        //// POST: Admission/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var admittedVM = await _context.AdmittedVM.FindAsync(id);
        //    if (admittedVM != null)
        //    {
        //        _context.AdmittedVM.Remove(admittedVM);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AdmittedVMExists(int id)
        {
            return _context.AdmittedPatients.Any(e => e.Id == id);
        }
    }
}
