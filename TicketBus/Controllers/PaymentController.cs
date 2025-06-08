using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using TicketBus.Data;
using TicketBus.Models;
using TicketBus.Services.Momo;

namespace TicketBus.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMomoService _momoService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IMomoService momoService, ApplicationDbContext context, ILogger<PaymentController> logger)
        {
            _momoService = momoService ?? throw new ArgumentNullException(nameof(momoService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() => View();

        public IActionResult PaymentSuccess()
        {
            return View("PaymentSuccess");
        }

        public async Task<IActionResult> PaymentCallback()
        {
            try
            {
                _logger.LogInformation("[PaymentCallback] Đang xử lý phản hồi thanh toán từ MoMo. Query: {Query}", JsonConvert.SerializeObject(Request.Query));

                var response = await _momoService.PaymentExecuteAsync(Request.Query);

                _logger.LogInformation("[PaymentCallback] Phản hồi từ MoMo: {Response}", JsonConvert.SerializeObject(response));

                if (response == null || response.PaymentStatus == "Error" || string.IsNullOrEmpty(response.BillCode))
                {
                    _logger.LogWarning("[PaymentCallback] Thanh toán thất bại hoặc phản hồi không hợp lệ. PaymentStatus: {PaymentStatus}, BillCode: {BillCode}",
                        response?.PaymentStatus, response?.BillCode);
                    return View("PaymentFailed");
                }

                // Tìm bill trong CSDL
                var bill = await _context.Bills
                    .Include(b => b.Tickets)
                    .FirstOrDefaultAsync(b => b.BillCode == response.BillCode);

                if (bill == null)
                {
                    _logger.LogError("[PaymentCallback] Không tìm thấy bill với BillCode: {BillCode}", response.BillCode);
                    return View("PaymentFailed");
                }

                // Cập nhật trạng thái bill và vé
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    bill.PaymentStatus = "Completed";

                    foreach (var ticket in bill.Tickets)
                    {
                        ticket.State = TicketState.DaThanhToan;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("[PaymentCallback] Cập nhật trạng thái bill và vé thành công. BillCode: {BillCode}, IdBill: {IdBill}",
                        bill.BillCode, bill.IdBill);
                    return View("PaymentSuccess");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "[PaymentCallback] Lỗi khi cập nhật trạng thái bill và vé. BillCode: {BillCode}", response.BillCode);
                    return View("PaymentError");
                }
            }
            catch (SecurityException ex)
            {
                _logger.LogError(ex, "[PaymentCallback] Lỗi bảo mật khi xử lý callback MoMo");
                return View("PaymentError");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PaymentCallback] Lỗi hệ thống khi xử lý callback MoMo");
                return View("PaymentError");
            }
        }

        [HttpPost]
        [Route("CreatePaymentUrl")]
        public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model, [FromForm] string SelectedSeats, [FromForm] int BillId)
        {
            _logger.LogInformation("[CreatePaymentUrl] Nhận yêu cầu tạo URL thanh toán. Model: {Model}, SelectedSeats: {SelectedSeats}, BillId: {BillId}",
                JsonConvert.SerializeObject(model), SelectedSeats, BillId);

            if (model == null || string.IsNullOrEmpty(SelectedSeats) || BillId <= 0)
            {
                _logger.LogWarning("[CreatePaymentUrl] Dữ liệu đầu vào không hợp lệ. Model: {Model}, SelectedSeats: {SelectedSeats}, BillId: {BillId}",
                    model != null, SelectedSeats, BillId);
                return BadRequest(new { message = "Thông tin đơn hàng, danh sách ghế hoặc BillId không hợp lệ." });
            }

            // Chuyển danh sách ghế từ chuỗi CSV thành danh sách
            model.SelectedSeat = SelectedSeats.Split(',').ToList();
            model.SeatQuantity = model.SelectedSeat.Count;

            // Kiểm tra bill tồn tại
            var bill = await _context.Bills
                .Include(b => b.Tickets)
                .FirstOrDefaultAsync(b => b.IdBill == BillId);

            if (bill == null)
            {
                _logger.LogError("[CreatePaymentUrl] Không tìm thấy bill với IdBill: {BillId}", BillId);
                return BadRequest(new { message = "Hóa đơn không tồn tại." });
            }

            // Cập nhật thông tin bill nếu cần
            model.BillCode = bill.BillCode;
            model.Total = bill.Total;
            model.SeatQuantity = bill.SeatQuantity;
            model.IdPassenger = bill.IdPassenger;

            // Lấy thông tin user và passenger
            var userEmail = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                _logger.LogError("[CreatePaymentUrl] Không tìm thấy thông tin user đăng nhập. Email: {Email}", userEmail);
                return BadRequest(new { message = "Bạn cần đăng nhập để thanh toán." });
            }

            var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (passenger == null || passenger.IdPassenger == 0)
            {
                _logger.LogError("[CreatePaymentUrl] Không tìm thấy IdPassenger cho user. UserId: {UserId}", user.Id);
                return BadRequest(new { message = "Không thể xác định hành khách." });
            }

            model.IdPassenger = passenger.IdPassenger;

            // Gọi MoMo để tạo URL thanh toán
            try
            {
                var response = await _momoService.CreatePaymentAsync(model);

                _logger.LogInformation("[CreatePaymentUrl] Phản hồi từ MoMo: {Response}", JsonConvert.SerializeObject(response));

                if (response == null || string.IsNullOrEmpty(response.PayUrl))
                {
                    _logger.LogWarning("[CreatePaymentUrl] Lỗi khi tạo liên kết thanh toán. Response: {Response}", JsonConvert.SerializeObject(response));
                    return BadRequest(new { message = "Lỗi xử lý thanh toán." });
                }

                _logger.LogInformation("[CreatePaymentUrl] Tạo liên kết thanh toán thành công. PayUrl: {PayUrl}", response.PayUrl);
                return Redirect(response.PayUrl); // Redirect trực tiếp để hiển thị mã QR
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CreatePaymentUrl] Lỗi khi gọi MoMo service");
                return BadRequest(new { message = "Lỗi xử lý thanh toán: " + ex.Message });
            }
        }
    }
}