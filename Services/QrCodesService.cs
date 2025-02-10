using AutoMapper;
using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Repositories;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace UrlShortenerApi01.Services
{
    public class QrCodesService : GenericService<QrCode>, IQrCodesService
    {
        private readonly IGenericRepository<QrCode> _qrCodeRepository;
        private readonly IShortLinksRepository _shortLinksRepository;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;  
        public QrCodesService(
            IGenericRepository<QrCode> qrCodeRepository,
            IShortLinksRepository shortLinksRepository,
            IMapper mapper)
            : base(qrCodeRepository)
        {
            _qrCodeRepository = qrCodeRepository;
            _shortLinksRepository = shortLinksRepository;
            _mapper = mapper;
            _baseUrl = "https://yourdomain.com";  
        }

    
        public async Task<QrCodeDto> GenerateQrCodeForShortLinkAsync(int shortLinkId)
        {
          
            var shortLink = await _shortLinksRepository.GetByIdAsync(shortLinkId);
            if (shortLink == null)
            {
                throw new Exception("Short link not found");
            }

           
            string url = $"{_baseUrl}/{shortLink.ShortCode}";

            
            byte[] qrCodeBytes = GenerateQrCode(url);
            string base64Image = Convert.ToBase64String(qrCodeBytes);

            
            QrCode qrRecord = new QrCode
            {
                ImgaePath = base64Image,
                Size = 250,  // Set a default size
                Format = "png",
                CreatedAt = DateTime.UtcNow,
                ShortLinksId = shortLink.Id,
                UserId = shortLink.UserId   
            };
                  
            await _qrCodeRepository.AddAsync(qrRecord);
            await _qrCodeRepository.SaveAsync();

            
            return _mapper.Map<QrCodeDto>(qrRecord);
        }

      
        public byte[] GenerateQrCode(string link)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);   
                }
               
            }
        }
    }
}
 