using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class VehicleType
    {
        [Key]
        public int IdType { get; set; }

        public string? TypeCode { get; set; }

        public string? NameType { get; set; }

        [Range(4, int.MaxValue, ErrorMessage = "SeatCount must be greater than 3")]
        public int SeatCount { get; set; }

        public VehicleTypeState State { get; set; }

        // Thiết lập quan hệ 1 - n với Coach
        public List<Coach> Coaches { get; set; } = new List<Coach>();

    }

    public enum VehicleTypeState
    {
        [Display(Name = "Hoạt động")]
        HoatDong = 0,

        [Display(Name = "Không hoạt động")]
        KhongHoatDong = 1
    }

}
