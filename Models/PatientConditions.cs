using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PatientConditions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }

        [Required]
        public int ConditionId { get; set; }

        [ForeignKey(nameof(ConditionId))]
        public Conditions? Condition { get; set; }
        public string? ConditionNotes { get; set; }
    }
}
