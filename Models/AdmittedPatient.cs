using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class AdmittedPatient
    {
        [Key]
        public int Id { get; set; }
        public int PatinetId { get; set; }

        [ForeignKey("PatinetId")]
        public virtual Patient? Patient { get; set; }
        public int WardId { get; set; }

        [ForeignKey("WardId")]
        public virtual Ward? Ward { get; set; }
        public int Bed { get; set; }
        public DateTime DateAdmitted { get; set; }
        public int SurgeonID { get; set; }

        [ForeignKey("SurgeonID")]
        public virtual Surgeon? Surgeon { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int SuburbId { get; set; }

        [ForeignKey("SuburbId")]
        public virtual Surbub? Suburb { get; set; }
    }
}
