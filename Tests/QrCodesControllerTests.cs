using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using UrlShortenerApi01.Controllers;
using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;
using Xunit;

namespace UrlShortenerApi01.Tests
{
    public class QrCodesControllerTests
    {
        private readonly Mock<IQrCodesService> _mockQrCodesService;
        private readonly IMapper _mapper;
        private readonly QrCodesController _controller;

        public QrCodesControllerTests()
        {
            _mockQrCodesService = new Mock<IQrCodesService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UrlShortenerApi01.Mappings.MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _controller = new QrCodesController(null, _mockQrCodesService.Object, _mapper);
        }

        [Fact]
        public async Task GenerateQrCode_ReturnsCreatedAtActionResult_WithQrCodeDto()
        {
            // Arrange
            int shortLinkId = 1;
            var qrCodeDto = new QrCodeDto { Id = 1, ImgaePath = "base64image", Format = "png", Size = 250 };
            _mockQrCodesService.Setup(s => s.GenerateQrCodeForShortLinkAsync(shortLinkId))
                .ReturnsAsync(qrCodeDto);

            // Act
            var result = await _controller.GenerateQrCode(shortLinkId);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnDto = Assert.IsType<QrCodeDto>(createdAtActionResult.Value);
            Assert.Equal("base64image", returnDto.ImgaePath);
        }
    }
}
