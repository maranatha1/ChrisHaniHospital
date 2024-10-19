using System.ComponentModel.DataAnnotations;

namespace ChrisHaniHospital.Models
{
    public class Allergies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Allergy Name")]
        public string AllergyName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
