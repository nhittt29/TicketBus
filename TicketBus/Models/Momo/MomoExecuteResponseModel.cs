using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class MomoExecuteResponseModel
{
    [JsonProperty("billCode")]
    public string BillCode { get; set; } // ✅ Dùng `BillCode` thay vì `IdBill`

    [JsonProperty("seatQuantity")]
    public int? SeatQuantity { get; set; }

    [JsonProperty("idPassenger")]
    public int? IdPassenger { get; set; } // ✅ Liên kết với hành khách

    [Range(0, double.MaxValue, ErrorMessage = "FinalTotal must be greater than or equal to 0")]
    [JsonProperty("finalTotal")]
    public decimal FinalTotal { get; set; } // ✅ Tổng tiền cuối cùng

    [JsonProperty("paymentStatus")]
    public string? PaymentStatus { get; set; } // ✅ Trạng thái thanh toán

    [Range(0, double.MaxValue, ErrorMessage = "Total must be greater than or equal to 0")]
    [JsonProperty("total")]
    public long Total { get; set; } // ✅ Nếu MoMo yêu cầu số nguyên
}
