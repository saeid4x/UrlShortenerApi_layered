using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;

namespace UrlShortenerApi01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        
        private readonly IGenericService<QrCode> _qrCodeService;
        private readonly IQrCodesService _qrCodesService;
        private readonly IMapper _mapper;
        private readonly ILogger<QrCodesController> _logger;

        public QrCodesController(ILogger<QrCodesController> logger, IQrCodesService qrCodesService, IMapper mapper)
        {
            _logger = logger;
            _qrCodesService = qrCodesService;
            _mapper = mapper;
        }

        // GET: api/QrCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QrCodeDto>>> GetAll()
        {
            var qrCodes = await _qrCodeService.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<QrCodeDto>>(qrCodes);
            return Ok(dtos);
        }

        // GET: api/QrCodes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QrCodeDto>> GetById(int id)
        {
            var qrCode = await _qrCodeService.GetByIdAsync(id);
            if (qrCode == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<QrCodeDto>(qrCode);
            return Ok(dto);
        }


        // POST: api/QrCodes/generate/{shortLinkId}
        [HttpPost("generate/{shortLinkId}")]
        public async Task<ActionResult<QrCodeDto>> GenerateQrCode(int shortLinkId)
        {
            var qrCodeDto = await _qrCodesService.GenerateQrCodeForShortLinkAsync(shortLinkId);
            return CreatedAtAction(nameof(GetById), new { id = qrCodeDto.Id }, qrCodeDto);
        }


        // POST: api/QrCodes
        [HttpPost]
        public async Task<ActionResult<QrCodeDto>> Create([FromBody] QrCodeDto dto)
        {
            var qrCode = _mapper.Map<QrCode>(dto);
            var created = await _qrCodeService.CreateAsync(qrCode);
            var createdDto = _mapper.Map<QrCodeDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // PUT: api/QrCodes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QrCodeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var qrCode = _mapper.Map<QrCode>(dto);
            await _qrCodeService.UpdateAsync(qrCode);
            return NoContent();
        }

        // DELETE: api/QrCodes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _qrCodeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("generate")]
        public IActionResult GenerateQrCode([FromQuery] string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return BadRequest("Link is required.");
            }

            // Generate QR code as byte array
            byte[] qrCodeBytes = _qrCodesService.GenerateQrCode(link);

            if (qrCodeBytes == null || qrCodeBytes.Length == 0)
            {
                return StatusCode(500, "QR Code generation failed.");
            }

            // Return the QR code as a PNG image
            return File(qrCodeBytes, "image/png");
        }

    }
}
