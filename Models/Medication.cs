using System.ComponentModel.DataAnnotations;

namespace ChrisHaniHospital.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Medication Name")]
        public string MedicationName { get; set; }

        [Required]
        public int Schedule { get; set; }

        [Required]
        [Display(Name = "Dosage Form")]
        public string Dosage { get; set; }
    }
}
