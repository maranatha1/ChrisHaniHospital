using System.ComponentModel.DataAnnotations;

namespace ChrisHaniHospital.Models
{
    public class Ward
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ward Name")]
        public string Name { get; set; }

        [Display(Name = "Number of Beds")]
        public int NumberOFBeds { get; set; }
    }
}
