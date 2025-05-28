using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class RegistForm
    {
        [Key]
        public int IdRegist { get; set; }
        public string? RegistCode { get; set; }
        [ForeignKey("Brand")]
        public int? IdBrand { get; set; }
        [Range(typeof(DateTime), "1900-01-01", "2025-04-06", ErrorMessage = "CreateDate must be on or before today")]
        public DateTime? CreateDate { get; set; }
        public RegistFormState State { get; set; }
        public string? Content { get; set; }
        public Brand? Brand { get; set; }
        public string? RejectReason { get; set; }
    }

    public enum RegistFormState
    {
        [Display(Name = "Chưa xử lý")]
        ChuaXuLy = 0,

        [Display(Name = "Đã xử lý")]
        DaXuLy = 1
    }

}
