using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortenerApi01.Models
{
    public class QrCode
    {
        public int Id { get; set; }
        public string  ImgaePath { get; set; }
        public int Size { get; set; }
        public string  Format { get; set; }
        public string  customLogo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

          
       
        public int ShortLinksId { get; set; }

        [ForeignKey("ShortLinksId")]
        [DeleteBehavior(DeleteBehavior.Restrict)] // Specify ON DELETE NO ACTION
        public ShortLinks ShortLinks { get; set; }


       
        public string  UserId { get; set; }

        [ForeignKey("UserId")]
        [DeleteBehavior(DeleteBehavior.Restrict)] // Specify ON DELETE NO ACTION
        public ApplicationUser User { get; set; }

    }
}
