using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PrescriptionMedicationAddVM
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public int Quantity { get; set; }
        public string? Instructions { get; set; } = string.Empty;
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
    }
}
