using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class City
    {
        [Key]
        public int IdCity { get; set; }
        public string? CityCode { get; set; }
        public string? NameCity { get; set; }

        public ICollection<District> Districts { get; set; } = new List<District>();
        public ICollection<BusRoute> StartRoutes { get; set; } = new List<BusRoute>();
        public ICollection<BusRoute> EndRoutes { get; set; } = new List<BusRoute>();
    }
}
