using Microsoft.EntityFrameworkCore;
using myshop.DataAccess;

namespace myshop.DAL.Repostiories
{
    public class GenericReposiotry<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericReposiotry(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> FindOneAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
