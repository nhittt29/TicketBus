using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class District
    {
        [Key]
        public int IdDistrict { get; set; }
        public string? DistrictCode { get; set; }
        public string? NameDistrict { get; set; }
        [ForeignKey("City")]
        public int? IdCity { get; set; }
        public City? City { get; set; }
    }
}
