using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }
        public string? EmployeeCode { get; set; }
        public string? NameEmployee { get; set; }
        public string? IdCard { get; set; }
        [Range(typeof(DateTime), "1900-01-01", "2025-04-06", ErrorMessage = "Birthday must be on or before today")]
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public EmployeeState State { get; set; }
        public string? Gender { get; set; } // Ghi chú: CHECK ([Gender]=N'Nữ' OR [Gender]=N'Nam') cần kiểm tra trong logic
        [ForeignKey("Brand")]
        public int? IdBrand { get; set; }
        [ForeignKey("Position")]
        public int? IdPos { get; set; }

        public Brand? Brand { get; set; }
        public Position? Position { get; set; }
    }
    public enum EmployeeState
    {
        [Display(Name = "Đang hoạt động")]
        DangHoatDong = 0,

        [Display(Name = "Không hoạt động")]
        KhongHoatDong = 1,

        [Display(Name = "Nghỉ phép")]
        NghiPhep = 2
    }

}
