using System.ComponentModel.DataAnnotations;

namespace TicketBus.Models
{
    public class TypeNews
    {
        [Key]
        public int IdTypeNews { get; set; }
        public string? TypeCode { get; set; }
        public string? NameTypeNews { get; set; }
    }
}
