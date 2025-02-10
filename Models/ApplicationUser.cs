using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortenerApi01.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } 

        // Foreign key to Membership
        public int MembershipId { get; set; }

        [ForeignKey("MembershipId")]
        public Membership Membership { get; set; }

        // Navigation properties
        public Profile Profile { get; set; }
        public ICollection<ShortLinks> ShortLinks { get; set; }
        public ICollection<QrCode> QrCodes { get; set; }


    }
}
