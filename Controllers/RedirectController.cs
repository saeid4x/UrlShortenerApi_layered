using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;

namespace UrlShortenerApi01.Controllers
{
    [AllowAnonymous]
    [Route("{shortCode}")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IShortLinksService _shortLinksService;
        private readonly IGenericService<ClickStat> _clickStatService;

        public RedirectController(
          IShortLinksService shortLinksService,
          IGenericService<ClickStat> clickStatService)
        {
            _shortLinksService = shortLinksService;
            _clickStatService = clickStatService;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
        {
            
            var shortLink = await _shortLinksService.GetByShortCodeAsync(shortCode);
            if (shortLink == null)
            {
                return NotFound("Short link not found.");
            }

            
            var clickStat = new ClickStat
            {
              
                shortLinkId = shortLink.Id,
                ClickTime = DateTime.UtcNow,
                
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                
                Browser = Request.Headers["User-Agent"].FirstOrDefault(),
             
                Referer = Request.Headers["Referer"].FirstOrDefault() ?? "Unknown",
                
                Country = "Unknown",
                City = "Unknown",
              
                Platform = "Unknown"
            };

            // Save the click stat record.
            await _clickStatService.CreateAsync(clickStat);

           
            shortLink.ClickCount += 1;
            await _shortLinksService.UpdateAsync(shortLink);

          
            return Redirect(shortLink.OriginUrl);
        }


        }
}
