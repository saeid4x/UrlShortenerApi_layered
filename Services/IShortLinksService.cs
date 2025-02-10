using UrlShortenerApi01.Models;

namespace UrlShortenerApi01.Services
{
    public interface IShortLinksService : IGenericService<ShortLinks>
    {
        Task<ShortLinks> GetByShortCodeAsync(string shortCode);
    }
}
