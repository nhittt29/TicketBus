using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models.ViewModels
{
    public class AddPriceViewModel
    {
        public int ScheduleId { get; set; }
        public string ScheduleInfo { get; set; }

        [Required(ErrorMessage = "Điểm bắt đầu là bắt buộc")]
        public int StartStopId { get; set; }

        [Required(ErrorMessage = "Điểm kết thúc là bắt buộc")]
        public int EndStopId { get; set; }

        [Required(ErrorMessage = "Giá vé là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0")]
        public decimal PriceValue { get; set; }

        public List<RouteStop> RouteStops { get; set; }
        public List<Price> ExistingPrices { get; set; }
    }
}
