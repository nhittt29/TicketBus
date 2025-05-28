using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Coach
    {
        [Key]
        public int IdCoach { get; set; }

        [StringLength(50)]
        public string? CoachCode { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^51D-\d{3}\.\d{2}$", ErrorMessage = "Biển số xe phải có định dạng 51D-123.45")]
        public string? NumberPlate { get; set; }

        public CoachState State { get; set; }

        [ForeignKey("VehicleType")]
        public int IdType { get; set; }
        public VehicleType? VehicleType { get; set; }

        public string Images { get; set; }

        public string Documents { get; set; }



        [ForeignKey("Brand")]
        public int IdBrand { get; set; }

        public Brand? Brand { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>(); // Khởi tạo để tránh null
    }

    public enum CoachState
    {
        [Display(Name = "Hoạt động")]
        HoatDong = 0,

        [Display(Name = "Không hoạt động")]
        KhongHoatDong = 1,

        [Display(Name = "Chờ phê duyệt")]
        ChoPheDuyet = 2,

        [Display(Name = "Đã phê duyệt")]
        DaPheDuyet = 3,

        [Display(Name = "Từ chối")]
        TuChoi = 4
    }
}
