namespace UrlShortenerApi01.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Basic";
        public int MaxShortLinks { get; set; }
        public int MaxQrCodes { get; set; }
        public bool  AllowCustomShortLinks { get; set; }
        public bool AllowAnalytics { get; set; }
        public bool  AllowExpiration { get; set; }
        public decimal MontlyPrice { get; set; }
        public DateTime CreateAt { get; set; }

      
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }
}
