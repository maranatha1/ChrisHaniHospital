using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChrisHaniHospital.Areas.Identity.Data;

namespace ChrisHaniHospital.Models
{
    public class Surgeon
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Registration Number")]
        public string Registration_Number { get; set; }
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ChrisHaniUser? User { get; set; }
    }
}
