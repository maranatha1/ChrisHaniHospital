using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChrisHaniHospital.Areas.Identity.Data;

namespace ChrisHaniHospital.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }

        [Required]
        public int? SuburbId { get; set; }

        [ForeignKey("SurburbId")]
        public virtual Surbub? Suburb { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ChrisHaniUser? User { get; set; }
    }
}
