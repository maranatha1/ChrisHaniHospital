using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province? Province { get; set; }
    }
}
