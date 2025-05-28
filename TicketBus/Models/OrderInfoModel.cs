using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class OrderInfoModel
    {
       

        [JsonProperty("billCode")]
        public string? BillCode { get; set; }

        [JsonProperty("seatQuantity")]
        public int? SeatQuantity { get; set; }

        [JsonProperty("idPassenger")]
        public int? IdPassenger { get; set; } // ✅ Liên kết với hành khách

        [Range(0, double.MaxValue, ErrorMessage = "Total must be greater than or equal to 0")]
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FinalTotal must be greater than or equal to 0")]
   

        [JsonProperty("paymentStatus")]
        public string? PaymentStatus { get; set; } // ✅ Trạng thái thanh toán
        public List<string> SelectedSeat { get; internal set; }
    }
}
