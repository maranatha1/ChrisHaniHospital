using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class PatientVitals
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient? Patient { get; set; }

        [Required]
        [Display(Name = "Blood Pressure")]
        public int BodyTemparature { get; set; }

        [Required]
        [Display(Name = "Body Temperature (C)")]
        public int BloodPressure { get; set; }

        [Required]
        [Display(Name = "Heart Rate")]
        public int HeartRate { get; set; }

        [Required]
        [Display(Name = "Respiratory Rate")]
        public int RespiratoryRate { get; set; }

        [Required]
        [Display(Name = "Oxygen Saturation")]
        public int OxygenSaturation { get; set; }

        [Required]
        [Display(Name = "Pulse rate")]
        public int PulseRate { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
