using UrlShortenerApi01.Models;

namespace UrlShortenerApi01.Repositories
{
    
        public interface IShortLinksRepository : IGenericRepository<ShortLinks>
        {
            Task<ShortLinks> GetByShortCodeAsync(string shortCode);
        }
    
}
