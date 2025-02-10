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
    public class ClickStatsController : ControllerBase
    {
        private readonly IGenericService<ClickStat> _clickStatService;
        private readonly IMapper _mapper;

        public ClickStatsController(IGenericService<ClickStat> clickStatService, IMapper mapper)
        {
            _clickStatService = clickStatService;
            _mapper = mapper;
        }

        // GET: api/ClickStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClickStatDto>>> GetAll()
        {
            var stats = await _clickStatService.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ClickStatDto>>(stats);
            return Ok(dtos);
        }

        // GET: api/ClickStats/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClickStatDto>> GetById(int id)
        {
            var stat = await _clickStatService.GetByIdAsync(id);
            if (stat == null) 
            {
                return NotFound();
            }
            var dto = _mapper.Map<ClickStatDto>(stat);
            return Ok(dto);
        }

        // POST: api/ClickStats
        [HttpPost]
        public async Task<ActionResult<ClickStatDto>> Create([FromBody] ClickStatDto dto)
        {
            var stat = _mapper.Map<ClickStat>(dto);
            var created = await _clickStatService.CreateAsync(stat);
            var createdDto = _mapper.Map<ClickStatDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // PUT: api/ClickStats/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClickStatDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var stat = _mapper.Map<ClickStat>(dto);
            await _clickStatService.UpdateAsync(stat);
            return NoContent();
        }

        // DELETE: api/ClickStats/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clickStatService.DeleteAsync(id);
            return NoContent();
        }
    }
}
