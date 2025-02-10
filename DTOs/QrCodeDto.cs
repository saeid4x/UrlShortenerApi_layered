namespace UrlShortenerApi01.DTOs
{
    public class QrCodeDto
    {
        public int Id { get; set; }
        public string ImgaePath { get; set; }
        public int Size { get; set; }
        public string Format { get; set; }
        public string CustomLogo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int ShortLinksId { get; set; }
        public string UserId { get; set; }
    }
}
