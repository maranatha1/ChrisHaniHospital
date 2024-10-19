using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisHaniHospital.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        [ForeignKey("CountryID")]
        public int CountryID { get; set; }
        public virtual Country? Country { get; set; }
    }
}
