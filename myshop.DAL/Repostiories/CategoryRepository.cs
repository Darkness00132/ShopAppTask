using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.DAL.Repostiories
{
    public class CategoryRepository : GenericReposiotry<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
