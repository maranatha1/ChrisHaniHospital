using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class AdministeredMedication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicationId { get; set; }

        [ForeignKey(nameof(MedicationId))]
        public virtual Medication? Medication { get; set; }

        [Required]
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public int PrescriptionId { get; set; }

        [ForeignKey(nameof(PrescriptionId))]
        public virtual Precription? Prescription { get; set; }
    }
}
