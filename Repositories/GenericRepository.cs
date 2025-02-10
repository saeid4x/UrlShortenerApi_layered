using Microsoft.EntityFrameworkCore;
using UrlShortenerApi01.Data;

namespace UrlShortenerApi01.Repositories
{
    
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected readonly ApplicationDbContext _context;
            protected readonly DbSet<T> _dbSet;
            private static readonly Random _random = new Random();


        public GenericRepository(ApplicationDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<T> GetByIdAsync(object id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
            }

            public void Update(T entity)
            {
                _dbSet.Update(entity);
            }

            public void Remove(T entity)
            {
                _dbSet.Remove(entity);
            }

            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }

        public async Task<string> GenerateShortCodeAsync()
        {
            string shortCode;

            do
            {
                shortCode = Guid.NewGuid().ToString("N").Substring(0, 6); // Generates a unique 6-character string
            } while (await _context.ShortLinks.AnyAsync(s => s.ShortCode == shortCode)); // Ensure uniqueness
            return shortCode;
        }

    }

}
