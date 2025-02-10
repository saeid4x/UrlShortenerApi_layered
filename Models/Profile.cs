namespace UrlShortenerApi01.Models
{
    public class Profile
    {
        public int Id { get; set; }        
        public string  Firstname { get; set; }
        public string  Lastname { get; set; }
        public string  AvatarUrl { get; set; }
        public string  Bio { get; set; }
        public string  Company { get; set; }
        public string  Website { get; set; }

        // Foreign key back to User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
