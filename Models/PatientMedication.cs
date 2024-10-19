using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PatientMedication
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication? Medication { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string? MedicationNotes { get; set; }
        public int Quantity { get; set; }
    }
}
