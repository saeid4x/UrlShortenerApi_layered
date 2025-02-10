using Microsoft.EntityFrameworkCore;
 
using UrlShortenerApi01.Data;
using UrlShortenerApi01.Models;

namespace UrlShortenerApi01.Repositories
{
    public class ShortLinksRepository : GenericRepository<ShortLinks>, IShortLinksRepository
    {
        public ShortLinksRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public async Task<ShortLinks> GetByShortCodeAsync(string shortCode)
        {
          
            return await _context.ShortLinks.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        }
    }
}
