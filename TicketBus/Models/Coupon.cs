using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Coupon
    {
        [Key]
        public int IdCoupon { get; set; }
        public string? CouponCode { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [Range(0.01, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        public decimal Percentage { get; set; }
    }
}
