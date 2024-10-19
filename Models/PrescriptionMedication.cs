using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PrescriptionMedication
    {
        [Key]
        public int Id { get; set; }
        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication? Medication { get; set; }
        public int Quantity { get; set; }
        public string? Instructions { get; set; } = string.Empty;
        public int PrescriptionId { get; set; }

        [ForeignKey("PrescriptionId")]
        public Precription? Prescription { get; set; }
    }
}
