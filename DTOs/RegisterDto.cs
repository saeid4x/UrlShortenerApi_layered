namespace UrlShortenerApi01.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; } // Optional additional info
        public string Password { get; set; }
        public int MembershipId { get; set; }
    }
}
