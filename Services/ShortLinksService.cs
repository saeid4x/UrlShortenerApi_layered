using UrlShortenerApi01.Models;
using UrlShortenerApi01.Repositories;

namespace UrlShortenerApi01.Services
{
    public class ShortLinksService : GenericService<ShortLinks>, IShortLinksService
    {
        private readonly IShortLinksRepository _shortLinksRepository;

        public ShortLinksService(IShortLinksRepository shortLinksRepository)
            : base(shortLinksRepository)
        {
            _shortLinksRepository = shortLinksRepository;
        }

        public async Task<ShortLinks> GetByShortCodeAsync(string shortCode)
        {
            return await _shortLinksRepository.GetByShortCodeAsync(shortCode);
        }
    }
}
