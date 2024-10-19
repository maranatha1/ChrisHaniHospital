using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class Surbub
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City? City { get; set; }
    }
}
