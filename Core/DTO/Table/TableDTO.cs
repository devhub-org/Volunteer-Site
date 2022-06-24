using Microsoft.AspNetCore.Http;

namespace Core.DTO
{
    public class TableDTO
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public IFormFile Image { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; } = true;
        public string AuthorId { get; set; }
    }
}
