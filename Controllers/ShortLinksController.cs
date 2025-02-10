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
    public class ShortLinksController : ControllerBase
    {
        private readonly IShortLinksService _shortLinksService;
        private readonly IMapper _mapper;

        public ShortLinksController(IShortLinksService shortLinksService, IMapper mapper)
        {
            _shortLinksService = shortLinksService;
            _mapper = mapper;
        }

        // GET: api/ShortLinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortLinksDto>>> GetAll()
        {
            var shortLinks = await _shortLinksService.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ShortLinksDto>>(shortLinks);
            return Ok(dtos);
        }

        // GET: api/ShortLinks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShortLinksDto>> GetById(int id)
        {
            var entity = await _shortLinksService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<ShortLinksDto>(entity);
            return Ok(dto);
        }

        // GET: api/ShortLinks/shortcode/{shortCode}
        [HttpGet("shortcode/{shortCode}")]
        public async Task<ActionResult<ShortLinksDto>> GetByShortCode(string shortCode)
        {
            var entity = await _shortLinksService.GetByShortCodeAsync(shortCode);
            if (entity == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<ShortLinksDto>(entity);
            return Ok(dto);
        }

        // POST: api/ShortLinks
        [HttpPost]
        public async Task<ActionResult<ShortLinksDto>> Create([FromBody] ShortLinksDto dto)
        {
            
            var entity = _mapper.Map<ShortLinks>(dto);
            var createdEntity = await _shortLinksService.CreateAsync(entity);
            var createdDto = _mapper.Map<ShortLinksDto>(createdEntity);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // PUT: api/ShortLinks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ShortLinksDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var entity = _mapper.Map<ShortLinks>(dto);
            await _shortLinksService.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/ShortLinks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shortLinksService.DeleteAsync(id);
            return NoContent();
        }
    }
}
