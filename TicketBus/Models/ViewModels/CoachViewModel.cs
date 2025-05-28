using Microsoft.AspNetCore.Http;

namespace TicketBus.Models.ViewModels
{
    public class CoachViewModel
    {
        public string CoachCode { get; set; }
        public string NumberPlate { get; set; }
        public int IdType { get; set; }
        public int IdBrand { get; set; }
        public int State { get; set; }

        // Danh sách file ảnh và tài liệu upload
        public List<IFormFile> ImageList { get; set; } = new List<IFormFile>();
        public List<IFormFile> DocumentList { get; set; } = new List<IFormFile>();
    }
}
