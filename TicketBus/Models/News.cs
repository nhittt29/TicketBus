using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class News
    {
        [Key]
        public int IdNews { get; set; }
        public string? NewsCode { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public NewsState State { get; set; }
        [Range(typeof(DateTime), "1900-01-01", "2025-04-06", ErrorMessage = "CreatedDate must be on or before today")]
        public DateTime? CreatedDate { get; set; }
        [ForeignKey("TypeNews")]
        public int? IdTypeNews { get; set; }

        public TypeNews? TypeNews { get; set; }
    }
    public enum NewsState
    {
        [Display(Name = "Hiện")]
        Hien = 0,

        [Display(Name = "Ẩn")]
        An = 1
    }
}
