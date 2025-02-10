namespace UrlShortenerApi01.DTOs
{
    public class MembershipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxShortLinks { get; set; }
        public int MaxQrCodes { get; set; }
        public bool AllowCustomShortLinks { get; set; }
        public bool AllowAnalytics { get; set; }
        public bool AllowExpiration { get; set; }
        public decimal MontlyPrice { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
