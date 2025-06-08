using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class DiscountDetails
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Coupon")]
        public int IdCoupon { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Bill")]
        public int IdBill { get; set; }
        public Coupon? Coupon { get; set; }
        public Bill? Bill { get; set; }
    }
}
