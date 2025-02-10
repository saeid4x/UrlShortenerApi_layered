namespace UrlShortenerApi01.DTOs
{
    public class ClickStatDto
    {
        public int Id { get; set; }
        public int ShortLinkId { get; set; }
        public DateTime ClickTime { get; set; }
        public string IPAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public string Referer { get; set; }
    }
}
