using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChrisHaniHospital.Areas.Identity.Data;

namespace ChrisHaniHospital.Models
{
    public class Anaesthesiologist
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }

        [Required]
        public string Registration_Number { get; set; }

        [ForeignKey("UserID")]
        public virtual ChrisHaniUser User { get; set; }
    }
}
