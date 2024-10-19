namespace ChrisHaniHospital.Models
{
    public class PrescriptionListVM
    {
        public Precription? Precription { get; set; }
        public List<PrescriptionMedication> Medications { get; set; }
    }
}
