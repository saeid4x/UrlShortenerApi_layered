namespace UrlShortenerApi01.Models
{
    public class ShortLinks
    {
        public int Id { get; set; }
        public string  OriginUrl { get; set; }
        public string  ShortCode { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int ClickCount { get; set; }
        public bool  IsActive { get; set; }
        public bool  IsCustomAlias { get; set; }

        public string UserId { get; set; }
        // Navigation properties
        public ApplicationUser User { get; set; }
        public ICollection<ClickStat> ClickStat { get; set; }


      
        public ICollection<QrCode> QrCode { get; set; }

      


    }
}
