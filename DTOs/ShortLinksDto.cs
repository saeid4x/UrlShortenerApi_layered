namespace UrlShortenerApi01.DTOs
{
    public class ShortLinksDto
    {
        public int Id { get; set; }
        public string OriginUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? DeletedAt { get; set; } = null;
        public int ClickCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsCustomAlias { get; set; }
        public string UserId { get; set; }
    }
}
