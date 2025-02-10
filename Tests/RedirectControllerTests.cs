using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UrlShortenerApi01.Controllers;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;
using Xunit;

namespace UrlShortenerApi01.Tests
{
    public class RedirectControllerTests
    {
        private readonly Mock<IShortLinksService> _mockShortLinksService;
        private readonly Mock<IGenericService<ClickStat>> _mockClickStatService;
        private readonly RedirectController _controller;

        public RedirectControllerTests()
        {
            _mockShortLinksService = new Mock<IShortLinksService>();
            _mockClickStatService = new Mock<IGenericService<ClickStat>>();

            _controller = new RedirectController(_mockShortLinksService.Object, _mockClickStatService.Object);

            // Set up a fake HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = IPAddress.Parse("8.8.8.8");
            httpContext.Request.Headers["User-Agent"] = "UnitTest-Agent";
            httpContext.Request.Headers["Referer"] = "https://referrer.com";
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        [Fact]
        public async Task RedirectToOriginalUrl_ReturnsNotFound_WhenShortLinkDoesNotExist()
        {
            // Arrange
            string testShortCode = "abc123";
            _mockShortLinksService.Setup(s => s.GetByShortCodeAsync(testShortCode))
                .ReturnsAsync((ShortLinks)null);

            // Act
            var result = await _controller.RedirectToOriginalUrl(testShortCode);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task RedirectToOriginalUrl_ReturnsRedirectResult_WhenShortLinkExists()
        {
            // Arrange
            string testShortCode = "abc123";
            var shortLink = new ShortLinks
            {
                Id = 1,
                OriginUrl = "https://example.com",
                ShortCode = testShortCode,
                ClickCount = 0,
                UserId = "user1"
            };
            _mockShortLinksService.Setup(s => s.GetByShortCodeAsync(testShortCode))
                .ReturnsAsync(shortLink);
            _mockClickStatService.Setup(s => s.CreateAsync(It.IsAny<ClickStat>()))
                .ReturnsAsync((ClickStat cs) => cs);

            _mockShortLinksService.Setup(s => s.UpdateAsync(shortLink)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RedirectToOriginalUrl(testShortCode);

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("https://example.com", redirectResult.Url);
        }
    }
}
