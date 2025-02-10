using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortenerApi01.Models
{
    public class ClickStat
    {
        public int Id { get; set; }
      
        public DateTime ClickTime { get; set; }  
        public string  IPAddress { get; set; }
        public string  Country { get; set; }
        public string City { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public string  Referer { get; set; }

        public int shortLinkId { get; set; }
      
        
        //Navigation property 
        [ForeignKey("shortLinkId")]
        public ShortLinks ShortLinks { get; set; }
    }
}
