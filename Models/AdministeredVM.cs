namespace ChrisHaniHospital.Models
{
    public class AdministeredVM
    {
        public IEnumerable<AdministeredMedication> AdministeredMedications { get; set; } = default!;
        public Precription? Precription { get; set; } = default!;
        public PrescriptionListVM? PrescriptionListVM { get; set; } = default!;
    }
}
