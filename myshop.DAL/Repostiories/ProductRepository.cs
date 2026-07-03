using Microsoft.EntityFrameworkCore;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.DAL.Repostiories
{
    public class ProductRepository : GenericReposiotry<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
