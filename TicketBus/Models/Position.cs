using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class Position
    {
        [Key]
        public int IdPos { get; set; }
        public string? PosCode { get; set; }
        public string? NamePos { get; set; }
        public PositionState State { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
    }

    public enum PositionState
    {
        [Display(Name = "Hoạt động")]
        HoatDong = 0,

        [Display(Name = "Không hoạt động")]
        KhongHoatDong = 1
    }
}
