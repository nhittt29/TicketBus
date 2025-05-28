using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Ticket
    {
        [Key]

        public int IdTicket { get; set; }
        public string? TicketCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DepartureDate { get; set; } // New field for departure date
        [ForeignKey("Seat")]
        public int? IdSeat { get; set; }
        [ForeignKey("Price")]
        public int? IdPrice { get; set; }
        [ForeignKey("Bill")]
        public int? IdBill { get; set; } // New foreign key to Bill
        public TicketState State { get; set; }
        [ForeignKey("Employee")]
        public int? IdEmployee { get; set; }

        public Seat? Seat { get; set; }
        public Price? Price { get; set; }
        public Bill? Bill { get; set; } // New navigation property to Bill
        public Employee? Employee { get; set; }

    }

    public enum TicketState
    {
        [Display(Name = "Chưa thanh toán")]
        ChuaThanhToan = 0,

        [Display(Name = "Đã thanh toán")]
        DaThanhToan = 1
    }
}
