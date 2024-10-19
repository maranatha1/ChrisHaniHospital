using System.ComponentModel.DataAnnotations;

namespace ChrisHaniHospital.Models
{
    public class Conditions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Condition Name")]
        public string ConditionName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
