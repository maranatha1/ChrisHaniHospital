using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class Precription
    {
        public int Id { get; set; }
        public DateTime DatePrescribed { get; set; } = DateTime.Now;
        public string? Status { get; set; } = "Pending";
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }
}
