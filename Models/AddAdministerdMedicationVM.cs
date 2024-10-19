namespace ChrisHaniHospital.Models
{
    public class AddAdministerdMedicationVM
    {
        public int MedicationId { get; set; }
        public int PrescriptionId { get; set; }
        public int AddAdministerd { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
