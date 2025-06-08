using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Services.Momo;

public class MomoService : IMomoService
{
    private readonly IOptions<MomoOptionModel> _options;
    private readonly ApplicationDbContext _context;

    public MomoService(IOptions<MomoOptionModel> options, ApplicationDbContext context)
    {
        _options = options;
        _context = context;
    }

    public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model)
    {
        model.BillCode ??= $"BC-{DateTime.UtcNow.Ticks}";
        model.SeatQuantity = model.SelectedSeat?.Count ?? 0;

        if (model.IdPassenger == null)
        {
            Console.WriteLine("❌ `IdPassenger` không hợp lệ.");
            return new MomoCreatePaymentResponseModel { PaymentStatus = "Error", Message = "Không thể xác định hành khách." };
        }

        var totalAmount = Convert.ToInt64(Math.Truncate(model.Total));
        Console.WriteLine($"📢 Giá trị `Total` sau khi chuyển đổi: {totalAmount}");

        var extraData = JsonConvert.SerializeObject(new
        {
            idPassenger = model.IdPassenger,
            seatQuantity = model.SeatQuantity
        });

        var rawSignature = $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.BillCode}&amount={totalAmount}&orderId={model.BillCode}&orderInfo=Thanh toán MoMo&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData={Convert.ToBase64String(Encoding.UTF8.GetBytes(extraData))}";

        var signature = ComputeHmacSha256(rawSignature, _options.Value.SecretKey);

        var requestData = new
        {
            accessKey = _options.Value.AccessKey,
            partnerCode = _options.Value.PartnerCode,
            requestType = _options.Value.RequestType,
            notifyUrl = _options.Value.NotifyUrl,
            returnUrl = _options.Value.ReturnUrl,
            orderId = model.BillCode,
            amount = totalAmount.ToString(),
            orderInfo = "Thanh toán MoMo",
            requestId = model.BillCode,
            extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(extraData)),
            signature = signature
        };

        Console.WriteLine($"📢 Kiểm tra `rawSignature` trước khi ký: {rawSignature}");
        Console.WriteLine($"📢 Chữ ký (`signature`) đã tạo: {signature}");
        Console.WriteLine($"📢 Dữ liệu trước khi gửi: {JsonConvert.SerializeObject(requestData)}");

        using var httpClient = new HttpClient();
        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_options.Value.MomoApiUrl, jsonContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"📢 Phản hồi từ MoMo: {responseContent}");

        return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(responseContent);
    }

    public async Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection)
    {
        Console.WriteLine($"📝 Bắt đầu xử lý phản hồi thanh toán từ MoMo...");
        Console.WriteLine($"📢 Dữ liệu nhận được: {JsonConvert.SerializeObject(collection)}");

        if (!collection.TryGetValue("orderId", out var orderIdValue) || string.IsNullOrEmpty(orderIdValue))
        {
            Console.WriteLine("❌ Lỗi: `BillCode` không hợp lệ.");
            return new MomoExecuteResponseModel { PaymentStatus = "Error" };
        }

        if (!collection.TryGetValue("amount", out var amountValue) ||
            string.IsNullOrEmpty(amountValue) || !decimal.TryParse(amountValue, out decimal total))
        {
            Console.WriteLine("❌ Lỗi: `Total` không hợp lệ.");
            return new MomoExecuteResponseModel { PaymentStatus = "Error" };
        }

        int? idPassenger = null;
        int? seatQuantity = null;

        if (collection.TryGetValue("extraData", out var extraDataValue) && !string.IsNullOrEmpty(extraDataValue))
        {
            try
            {
                var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(extraDataValue));
                var extraInfo = JsonConvert.DeserializeObject<dynamic>(decodedData);
                idPassenger = (int?)extraInfo.idPassenger;
                seatQuantity = (int?)extraInfo.seatQuantity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi giải mã `extraData`: {ex.Message}");
            }
        }

        Console.WriteLine($"✅ Dữ liệu giao dịch hợp lệ. BillCode: {orderIdValue}, Tổng tiền: {total}, Hành khách: {idPassenger}, Số ghế: {seatQuantity}");

        try
        {
            // ✅ Tính `FinalTotal` trước khi lưu
            var finalTotal = total; // 🔹 Ví dụ: Thêm 5% phí dịch vụ
            Console.WriteLine($"📢 Giá trị `FinalTotal` trước khi lưu: {finalTotal}");

            var bill = new Bill
            {
                BillCode = orderIdValue,
                Total = (long)finalTotal,   // ✅ Cập nhật `Total` thành `FinalTotal`
                FinalTotal = (long)finalTotal, // ✅ Lưu `FinalTotal` riêng biệt
                IdPassenger = idPassenger ?? 1,
                SeatQuantity = seatQuantity ?? 1,
                PaymentStatus = "Success",
            };

            Console.WriteLine($"📢 Kiểm tra trước khi lưu vào DB: BillCode = {orderIdValue}, FinalTotal = {finalTotal}, IdPassenger = {idPassenger}, SeatQuantity = {seatQuantity}");

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            Console.WriteLine($"✅ Đã lưu giao dịch vào DB với BillCode: {bill.BillCode}, FinalTotal: {finalTotal}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi khi lưu giao dịch vào DB: {ex.Message}");
            return new MomoExecuteResponseModel { PaymentStatus = "Error" };
        }


        return new MomoExecuteResponseModel
        {
            BillCode = orderIdValue,
            Total = (long)total,
            IdPassenger = idPassenger,
            SeatQuantity = seatQuantity,
            PaymentStatus = "Success",
        };
    }

    private string ComputeHmacSha256(string message, string secretKey)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "").ToLower();
    }
}
