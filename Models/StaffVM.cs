using ChrisHaniHospital.Areas.Identity.Data;

namespace ChrisHaniHospital.Models
{
    public class StaffVM
    {
        public List<Anaesthesiologist>? Anaesthesiologist { get; set; }
        public List<Nurse>? Nurse { get; set; }
        public List<Pharmacist>? Pharmacist { get; set; }
        public List<Surgeon>? Surgeon { get; set; }
    }
}
