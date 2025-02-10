using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortenerApi01.Controllers;
using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;
using Xunit;

namespace UrlShortenerApi01.Tests
{
    public class ShortLinksControllerTests
    {
        private readonly Mock<IShortLinksService> _mockShortLinksService;
        private readonly IMapper _mapper;
        private readonly ShortLinksController _controller;

        public ShortLinksControllerTests()
        {
            _mockShortLinksService = new Mock<IShortLinksService>();

            // Configure AutoMapper with the MappingProfile from your project.
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UrlShortenerApi01.Mappings.MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _controller = new ShortLinksController(_mockShortLinksService.Object, _mapper);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenShortLinkDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockShortLinksService.Setup(s => s.GetByIdAsync(testId))
                .ReturnsAsync((ShortLinks)null);

            // Act
            var result = await _controller.GetById(testId);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithShortLinkDto()
        {
            // Arrange
            int testId = 1;
            var shortLink = new ShortLinks
            {
                Id = testId,
                OriginUrl = "https://example.com",
                ShortCode = "abc123",
                ClickCount = 0,
                UserId = "user1"
            };
            _mockShortLinksService.Setup(s => s.GetByIdAsync(testId))
                .ReturnsAsync(shortLink);

            // Act
            var actionResult = await _controller.GetById(testId);
            var okResult = actionResult.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var dto = okResult.Value as ShortLinksDto;

            // Assert
            Assert.NotNull(dto);
            Assert.Equal("abc123", dto.ShortCode);
        }
    }
}
