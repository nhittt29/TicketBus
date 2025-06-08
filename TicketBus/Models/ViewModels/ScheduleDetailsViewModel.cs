using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models.ViewModels
{
    public class ScheduleDetailsViewModel
    {
        [Required(ErrorMessage = "Tuyến xe là bắt buộc")]
        public int IdCoach { get; set; }

        [Required(ErrorMessage = "Xe là bắt buộc")]
        public int IdRoute { get; set; }

        // Giờ khởi hành
        [Required(ErrorMessage = "Giờ khởi hành là bắt buộc")]
        [Range(0, 23, ErrorMessage = "Giờ khởi hành phải từ 0 đến 23")]
        public int DepartHour { get; set; }

        [Required(ErrorMessage = "Phút khởi hành là bắt buộc")]
        [Range(0, 59, ErrorMessage = "Phút khởi hành phải từ 0 đến 59")]
        public int DepartMinute { get; set; }

        // Giờ đến
        [Required(ErrorMessage = "Giờ đến là bắt buộc")]
        [Range(0, 23, ErrorMessage = "Giờ đến phải từ 0 đến 23")]
        public int ArriveHour { get; set; }

        [Required(ErrorMessage = "Phút đến là bắt buộc")]
        [Range(0, 59, ErrorMessage = "Phút đến phải từ 0 đến 59")]
        public int ArriveMinute { get; set; }

        // Không cần [Required] cho danh sách dropdown
        public List<SelectListItem> Routes { get; set; }
        public List<SelectListItem> Coaches { get; set; }

        // Thuộc tính TimeSpan vẫn cần để binding với model
        public TimeSpan DepartTime => new TimeSpan(DepartHour, DepartMinute, 0);
        public TimeSpan ArriveTime => new TimeSpan(ArriveHour, ArriveMinute, 0);
    }
}
