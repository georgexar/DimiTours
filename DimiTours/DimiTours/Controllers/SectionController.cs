using DimiTours.Data;
using DimiTours.Dto.Section;
using DimiTours.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/section")]
    public class SectionController : ControllerBase
    {
        private readonly DataContext _context;

        public SectionController(DataContext context) 
        {
            _context = context;
        }

        // GET: api/v1/section
        [HttpGet("all")]
        public IActionResult GetAllSections()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var sections = _context.Sections.ToList();

            var response = sections.Select(s => new SectionResponseDto
            {
                Id = s.Id,
                Title = s.Title,
                Purpose = s.Purpose,
                ContentText = s.ContentText,
                MediaItems = s.MediaItems,
                ImageUrl = s.ImageUrl
            }).ToList();

            return Ok(response);
        }

        // GET: api/v1/section/{id}
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetSectionById(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var section = _context.Sections.Find(id);
            if (section == null)
                return NotFound();

            var response = new SectionResponseDto
            {
                Id = section.Id,
                Title = section.Title,
                Purpose = section.Purpose,
                ContentText = section.ContentText,
                MediaItems = section.MediaItems,
                ImageUrl = section.ImageUrl

            };

            return Ok(response);
        }

        // POST: api/v1/section/create
        [HttpPost("create")]
        public IActionResult CreateSection([FromBody] CreateSectionDto dto)
        {
            var section = new Section
            {
                ImageUrl = dto.ImageUrl,
                Title = dto.Title,
                Purpose = dto.Purpose,
                ContentText = dto.ContentText,
                MediaItems = dto.MediaItems
            };

            _context.Sections.Add(section);
            _context.SaveChanges();

            var response = new SectionResponseDto
            {
                Id = section.Id,
                Title = section.Title,
                Purpose = section.Purpose,
                ContentText = section.ContentText
            };

            return CreatedAtAction(nameof(GetSectionById), new { id = section.Id }, response);
        }

        // DELETE: api/v1/section/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteSection(int id)
        {
            var section = _context.Sections.Find(id);
            if (section == null)
                return NotFound();

            _context.Sections.Remove(section);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
