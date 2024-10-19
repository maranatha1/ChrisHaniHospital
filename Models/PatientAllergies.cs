using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PatientAllergies
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        public int AllergyId { get; set; }

        [ForeignKey("AllergyId")]
        public Allergies? Allergies { get; set; }

        public string AllergyNotes { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }
    }
}
