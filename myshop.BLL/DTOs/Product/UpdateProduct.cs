using Microsoft.AspNetCore.Http;

namespace myshop.BLL.DTOs.Product
{
    public class UpdateProduct
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }

        public IFormFile? File { get; set; }
    }
}
