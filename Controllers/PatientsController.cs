using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;
using ChrisHaniHospital.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Controllers
{
    public class PatientsController : Controller
    {
        public readonly ChrisHaniContext _context;

        public PatientsController(ChrisHaniContext context)
        {
            _context = context;
        }

        public IActionResult Booked()
        {
            var Patinets = _context
                .Patients.Include(a => a.User)
                .ToList()
                .OrderBy(a => a.User.FirstName);

            ViewBag.Patients = Patinets;
            return View();
        }

        public IActionResult File(int? PatientID)
        {
            var patient = _context
                .Patients.Include(a => a.User)
                .Include(a => a.Suburb)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Province)
                .ThenInclude(a => a.Country)
                .Where(a => a.PatientId == PatientID)
                .FirstOrDefault();
            var patientVitals = _context
                .PatientVitals.Include(a => a.Patient)
                .Where(a => a.PatientId == PatientID)
                .OrderByDescending(a => a.DateRecorded)
                .ToList();
            var AdmittedPatient = _context
                .AdmittedPatients.Include(a => a.Patient)
                .Include(a => a.Surgeon)
                .ThenInclude(a => a.User)
                .Include(a => a.Ward)
                .Where(a => a.PatinetId == PatientID)
                .OrderByDescending(a => a.DateAdmitted)
                .ToList();
            var Prescriptions = _context
                .Precription.Where(a => a.PatientId == PatientID)
                .OrderByDescending(a => a.DatePrescribed)
                .ToList();
            var PatientPrescription = _context
                .Precription.Where(a => a.PatientId == PatientID && a.Status != "Default")
                .Select(a => new PrescriptionListVM
                {
                    Precription = a,
                    Medications = _context
                        .PrescriptionMedications.Include(pm => pm.Medication)
                        .Where(pm => pm.PrescriptionId == a.Id)
                        .ToList()
                })
                .OrderBy(a => a.Precription.DatePrescribed);
            PatientVM patientVM = new PatientVM
            {
                Patient = patient,
                PatientVitals = patientVitals,
                AdmittedPatient = AdmittedPatient,
                Precriptions = PatientPrescription.ToList()
            };
            return View(patientVM);
        }

        public async Task<IActionResult> RetakeVitals(
            int PatientID,
            int BloodPressure,
            int BodyTemparature,
            int HeartRate,
            int OxygenSaturation,
            int RespiratoryRate,
            int PulseRate
        )
        {
            if (
                PatientID == 0
                && BloodPressure == 0
                && BodyTemparature == 0
                && HeartRate == 0
                && OxygenSaturation == 0
                && RespiratoryRate == 0
                && PulseRate == 0
            )
            {
                TempData["Error"] = "Please enter the patient vitals";
                return RedirectToAction("File", new { PatientID = PatientID });
            }
            PatientVitals patientVitals = new PatientVitals
            {
                PatientId = PatientID,
                BodyTemparature = BodyTemparature,
                BloodPressure = BloodPressure,
                HeartRate = HeartRate,
                RespiratoryRate = RespiratoryRate,
                OxygenSaturation = OxygenSaturation,
                PulseRate = PulseRate,
                DateRecorded = DateTime.Now
            };
            _context.PatientVitals.Add(patientVitals);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Vitals recorded successfully";
            return RedirectToAction("File", new { PatientID = PatientID });
        }
    }
}
