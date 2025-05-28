using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBus.Models
{
    public class Bill
    {
        [Key]
        public int IdBill { get; set; }

        public string? BillCode { get; set; }

        public int? SeatQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total must be greater than or equal to 0")]
        public long Total { get; set; } // ✅ Nếu MoMo yêu cầu số nguyên

        public decimal FinalTotal { get; set; } // ✅ Tổng tiền cuối cùng sau giảm giá

        public string? PaymentStatus { get; set; }

        [ForeignKey("Passenger")]
        public int? IdPassenger { get; set; }
        public Passenger? Passenger { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
