using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models.ViewModels
{
    public class AvailableTripViewModel
    {
        [Display(Name = "Tuyến đường")]
        public string RouteName { get; set; }

        [Display(Name = "Điểm xuất phát")]
        public string StartCity { get; set; }

        [Display(Name = "Điểm đến")]
        public string EndCity { get; set; }

        [Display(Name = "Thời gian khởi hành")]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Nhà xe")]
        public string BrandName { get; set; }

        [Display(Name = "Loại xe")]
        public string VehicleType { get; set; }

        [Display(Name = "Tổng số ghế")]
        public int TotalSeats { get; set; }

        [Display(Name = "Số ghế còn trống")]
        public int AvailableSeats { get; set; }
    }
}
