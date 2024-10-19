using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Controllers.apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class Main : ControllerBase
    {
        private readonly ChrisHaniContext _context;

        public Main(ChrisHaniContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("GetCityAndProvince")]
        public IActionResult GetCityAndProvince([FromForm] int suburbId)
        {
            var suburb = _context
                .Surbub.Include(a => a.City)
                .ThenInclude(a => a.Province)
                .FirstOrDefault(s => s.Id == suburbId);
            if (suburb != null)
            {
                var result = new { City = suburb.City.Name, Province = suburb.City.Province.Name };
                return Ok(result);
            }
            return BadRequest("Suburb not found");
        }

        [HttpPost]
        [Route("GetWardID")]
        public IActionResult GetWardID([FromForm] int WardID)
        {
            var ward = _context.Ward.FirstOrDefault(s => s.Id == WardID);
            if (ward != null)
            {
                var result = new { ward = ward };
                return Ok(result);
            }
            return BadRequest("Suburb not found");
        }

        [HttpPost]
        [Route("AddPatientAllergies")]
        public async Task<IActionResult> AddAllergies(PatientAllergies patientAllergies)
        {
            try
            {
                patientAllergies.DateAdded = DateTime.Now;
                await _context.PatientAllergies.AddAsync(patientAllergies);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PrescriptionMedication")]
        public async Task<IActionResult> PrescriptionMedication(
            PrescriptionMedicationAddVM prescriptionMedication
        )
        {
            try
            {
                if (prescriptionMedication.PrescriptionId == 0)
                {
                    var prescription = new Precription
                    {
                        DatePrescribed = DateTime.Now,
                        Status = "Default",
                        PatientId = prescriptionMedication.PatientId
                    };
                    await _context.Precription.AddAsync(prescription);
                    await _context.SaveChangesAsync();
                    prescriptionMedication.PrescriptionId = prescription.Id;
                }
                var PreMEd = new PrescriptionMedication
                {
                    MedicationId = prescriptionMedication.MedicationId,
                    PrescriptionId = prescriptionMedication.PrescriptionId,
                    Quantity = prescriptionMedication.Quantity,
                    Instructions = prescriptionMedication.Instructions
                };
                await _context.PrescriptionMedications.AddAsync(PreMEd);
                await _context.SaveChangesAsync();
                return Ok(new { id = prescriptionMedication.PrescriptionId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddministerMedication")]
        public async Task<IActionResult> AddministerMedication(
            AddAdministerdMedicationVM prescriptionMedication
        )
        {
            try
            {
                var PreMEd = new AdministeredMedication
                {
                    MedicationId = prescriptionMedication.MedicationId,
                    AdministeredDate = DateTime.Now,
                    Quantity = prescriptionMedication.Quantity,
                    Notes = prescriptionMedication.Notes,
                    PrescriptionId = prescriptionMedication.PrescriptionId
                };
                await _context.administeredMedications.AddAsync(PreMEd);
                await _context.SaveChangesAsync();
                return RedirectToAction(
                    "Index",
                    "AdministeredMedications",
                    new { PresciptionId = prescriptionMedication.PrescriptionId }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddPatientCondition")]
        public async Task<IActionResult> AddCondition(PatientConditions conditions)
        {
            try
            {
                await _context.PatientConditions.AddAsync(conditions);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddPatientMedication")]
        public async Task<IActionResult> AddMedication(PatientMedication conditions)
        {
            try
            {
                await _context.PatientMedications.AddAsync(conditions);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemovePatientAllergy/{patientId}/{allergyId}")]
        public async Task<IActionResult> RemovePatientAllergy(int patientId, int allergyId)
        {
            var patientAllergy = await _context.PatientAllergies.FirstOrDefaultAsync(pa =>
                pa.PatientId == patientId && pa.AllergyId == allergyId
            );
            if (patientAllergy == null)
            {
                return NotFound(new { message = "Allergy not found" });
            }

            _context.PatientAllergies.Remove(patientAllergy);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Allergy removed successfully" });
        }

        [HttpDelete("RemoveMedication/{patientId}/{allergyId}")]
        public async Task<IActionResult> RemoveMedication(int patientId, int allergyId)
        {
            var patientAllergy = await _context.PrescriptionMedications.FirstOrDefaultAsync(pa =>
                pa.MedicationId == patientId && pa.PrescriptionId == allergyId
            );
            if (patientAllergy == null)
            {
                return NotFound(new { message = "Medication not found" });
            }

            _context.PrescriptionMedications.Remove(patientAllergy);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Medication removed successfully" });
        }

        [HttpPost("Administer/{patientId}/{PrescriptionId}")]
        public async Task<IActionResult> Administer(int patientId, int PrescriptionId)
        {
            var prescription = await _context.Precription.FirstOrDefaultAsync(pa =>
                pa.PatientId == patientId && pa.Id == PrescriptionId
            );
            if (prescription == null)
            {
                return NotFound(new { message = "prescription not found" });
            }
            prescription.Status = "Administered";
            _context.Precription.Update(prescription);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("RemovePatientMedication/{patientId}/{MedicationId}")]
        public async Task<IActionResult> RemovePatientMedication(int patientId, int MedicationId)
        {
            var patientMedication = await _context.PatientMedications.FirstOrDefaultAsync(pa =>
                pa.PatientId == patientId && pa.MedicationId == MedicationId
            );
            if (patientMedication == null)
            {
                return NotFound(new { message = "patient Medication not found" });
            }

            _context.PatientMedications.Remove(patientMedication);
            await _context.SaveChangesAsync();
            return Ok(new { message = "patient Medication removed successfully" });
        }

        [HttpDelete("RemovePatientCondition/{patientId}/{ConditionId}")]
        public async Task<IActionResult> RemovePatientCondition(int patientId, int ConditionId)
        {
            var patientConditions = await _context.PatientConditions.FirstOrDefaultAsync(pa =>
                pa.PatientId == patientId && pa.ConditionId == ConditionId
            );
            if (patientConditions == null)
            {
                return NotFound(new { message = "patient Conditions not found" });
            }

            _context.PatientConditions.Remove(patientConditions);
            await _context.SaveChangesAsync();
            return Ok(new { message = "patient Condition removed successfully" });
        }
    }
}
