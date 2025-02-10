using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortenerApi01.DTOs;
using UrlShortenerApi01.Models;
using UrlShortenerApi01.Services;
using Profile = UrlShortenerApi01.Models.Profile;


namespace UrlShortenerApi01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfilesController : ControllerBase
    {
        private readonly IGenericService<Profile> _profileService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfilesController(IGenericService<Profile> profileService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor; 
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetAll()
        {
            var profiles = await _profileService.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ProfileDto>>(profiles);
            return Ok(dtos);
        }

        // GET: api/Profiles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetById(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<ProfileDto>(profile);
            return Ok(dto);
        }

        // POST: api/Profiles
        [HttpPost]
        public async Task<ActionResult<ProfileDto>> Create([FromBody] ProfileDto dto)
        {

            // Get User ID from the authenticated user
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            // Map DTO to Profile entity and assign UserId automatically
            var profile = _mapper.Map<Profile>(dto);
            profile.UserId = userId; // Assign the authenticated UserId


            var created = await _profileService.CreateAsync(profile);
            var createdDto = _mapper.Map<ProfileDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // PUT: api/Profiles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfileDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var profile = _mapper.Map<Profile>(dto);
            await _profileService.UpdateAsync(profile);
            return NoContent();
        }

        // DELETE: api/Profiles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _profileService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("generate-shortcode")]
        public async Task<ActionResult<string>> GenerateShortCode()
        {
            var shortCode = await _profileService.GenerateShortCodeAsync();
            return Ok(shortCode);
        }
    }
}
