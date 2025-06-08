using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Pickup
    {
        [Key]
        public int IdPickup { get; set; }

        public string? PickupName { get; set; }

        [ForeignKey("City")]
        public int? IdCity { get; set; }

        [ForeignKey("Brand")]
        public int IdBrand { get; set; }

        [ForeignKey("BusRoute")]
        public int? IdRoute { get; set; }

        public City? City { get; set; }
        public Brand Brand { get; set; }
        public BusRoute? BusRoute { get; set; }
    }
}
