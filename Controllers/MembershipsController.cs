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
    public class MembershipsController : ControllerBase
    {
        private readonly IGenericService<Membership> _membershipService;
        private readonly IMapper _mapper;

        public MembershipsController(IGenericService<Membership> membershipService, IMapper mapper)
        {
            _membershipService = membershipService;
            _mapper = mapper;
        }

        // GET: api/Memberships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipDto>>> GetAll()
        {
            var memberships = await _membershipService.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<MembershipDto>>(memberships);
            return Ok(dtos);
        }

        // GET: api/Memberships/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipDto>> GetById(int id)
        {
            var membership = await _membershipService.GetByIdAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<MembershipDto>(membership);
            return Ok(dto);
        }

        // POST: api/Memberships
        [HttpPost]
        public async Task<ActionResult<MembershipDto>> Create([FromBody] MembershipDto dto)
        {
            var membership = _mapper.Map<Membership>(dto);
            var created = await _membershipService.CreateAsync(membership);
            var createdDto = _mapper.Map<MembershipDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // PUT: api/Memberships/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MembershipDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var membership = _mapper.Map<Membership>(dto);
            await _membershipService.UpdateAsync(membership);
            return NoContent();
        }

        // DELETE: api/Memberships/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _membershipService.DeleteAsync(id);
            return NoContent();
        }
    }
}
