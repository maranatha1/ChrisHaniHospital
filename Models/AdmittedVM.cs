using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class AdmittedVM
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string IdentityNumber { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public int Bed { get; set; }
        public int WardId { get; set; }

        public DateTime DateAdmitted { get; set; }
        public int SurgeonID { get; set; }

        [Required]
        [Display(Name = "AddressLine 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine 2")]
        public string? AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }

        public Surbub? Suburb { get; set; }

        public int AlergyID { get; set; }

        public string? AllergiesNote { get; set; }
        public int ConditionID { get; set; }

        public string? ConditionNote { get; set; }
        public int MedicationID { get; set; }

        public string? Instruction { get; set; }
        public int Quantity { get; set; }

        // Vitals
        [Required]
        public int PatientId { get; set; }

        [Required]
        [Display(Name = "Blood Pressure")]
        public int BodyTemparature { get; set; }

        [Required]
        [Display(Name = "Body Temperature (C)")]
        public int BloodPressure { get; set; }

        [Required]
        [Display(Name = "Heart Rate")]
        public int HeartRate { get; set; }

        [Required]
        [Display(Name = "Respiratory Rate")]
        public int RespiratoryRate { get; set; }

        [Required]
        [Display(Name = "Oxygen Saturation")]
        public int OxygenSaturation { get; set; }

        [Required]
        [Display(Name = "Pulse rate")]
        public int PulseRate { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
