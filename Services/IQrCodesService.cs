using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;

namespace UrlShortenerApi01.Services
{
    public interface IQrCodesService : IGenericService<QrCode>
    { 
        Task<QrCodeDto> GenerateQrCodeForShortLinkAsync(int shortLinkId); 
        byte[] GenerateQrCode(string link);
    }
}
