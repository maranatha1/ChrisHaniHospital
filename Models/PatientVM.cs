namespace ChrisHaniHospital.Models
{
    public class PatientVM
    {
        public List<PatientVitals>? PatientVitals { get; set; }
        public List<AdmittedPatient>? AdmittedPatient { get; set; }
        public List<PrescriptionListVM>? Precriptions { get; set; }
        public Patient? Patient { get; set; }
    }
}
