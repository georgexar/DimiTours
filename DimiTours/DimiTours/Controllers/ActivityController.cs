using DimiTours.Data;
using DimiTours.Dto.Activity;
using DimiTours.Dto.AnswerDto;
using DimiTours.Dto.QuestionDto;
using DimiTours.Dto.Section;
using DimiTours.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/activity")]
    public class ActivityController : Controller
    {
        private readonly DataContext _context;

        public ActivityController(DataContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        [Authorize]
        public IActionResult GetAllActivities()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var activities = _context.Activities
                .Include(a => a.Section)
                .Include(a => a.Questions)
                    .ThenInclude(q => q.Answers)
                .ToList();

            var response = activities.Select(activity => new ActivityResponseDto
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Section = new SectionResponseDto
                {
                    Id = activity.Section.Id,
                    ImageUrl = activity.Section.ImageUrl,
                    Title = activity.Section.Title,
                    Purpose = activity.Section.Purpose,
                    ContentText = activity.Section.ContentText,
                    MediaItems = activity.Section.MediaItems
                },
                Questions = activity.Questions.Select(q => new QuestionResponseDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Answers = q.Answers.Select(ans => new AnswerResponseDto
                    {
                        Id = ans.Id,
                        Text = ans.Text,
                        IsCorrect = ans.IsCorrect,
                        QuestionId = ans.QuestionId

                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        // GET BY ID
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetActivityById(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var activity = _context.Activities
                .Include(a => a.Section)
                .Include(a => a.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(a => a.Id == id);

            if (activity == null)
                return NotFound();

            var response = new ActivityResponseDto
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Section = new SectionResponseDto
                {
                    Id = activity.Section.Id,
                    ImageUrl = activity.Section.ImageUrl,
                    Title = activity.Section.Title,
                    Purpose = activity.Section.Purpose,
                    ContentText = activity.Section.ContentText,
                    MediaItems = activity.Section.MediaItems
                },
                Questions = activity.Questions.Select(q => new QuestionResponseDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Answers = q.Answers.Select(ans => new AnswerResponseDto
                    {
                        Id = ans.Id,
                        Text = ans.Text,
                        IsCorrect = ans.IsCorrect,
                        QuestionId = ans.QuestionId
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        // GET BY SECTION
        [HttpGet("/api/v1/sections/{sectionId}/activities")]
        public IActionResult GetActivitiesBySectionId(int sectionId)
        {
            var activities = _context.Activities
                .Include(a => a.Section)
                .Include(a => a.Questions)
                    .ThenInclude(q => q.Answers)
                .Where(a => a.SectionId == sectionId)
                .ToList();

            var response = activities.Select(a => new ActivityResponseDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Section = new SectionResponseDto
                {
                    Id = a.Section.Id,
                    ImageUrl = a.Section.ImageUrl,
                    Title = a.Section.Title,
                    Purpose = a.Section.Purpose,
                    ContentText = a.Section.ContentText,
                    MediaItems = a.Section.MediaItems
                },
                Questions = a.Questions.Select(q => new QuestionResponseDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    Type = q.Type,
                    Answers = q.Answers.Select(ans => new AnswerResponseDto
                    {
                        Id = ans.Id,
                        Text = ans.Text,
                        IsCorrect = ans.IsCorrect,
                        QuestionId = ans.QuestionId
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        // CREATE
        [HttpPost]
        public IActionResult CreateActivity([FromBody] CreateActivityDto dto)
        {
            var section = _context.Sections.Find(dto.SectionId);
            if (section == null)
                return BadRequest("Invalid SectionId");

            var questions = _context.Questions
                .Where(q => dto.QuestionIds.Contains(q.Id))
                .ToList();

            var activity = new Models.Activity
            {
                Title = dto.Title,
                Description = dto.Description,
                SectionId = dto.SectionId,
                Questions = questions
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, null);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _context.Activities
                .Include(a => a.Section)
                .Include(a => a.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(a => a.Id == id);

            if (activity == null)
                return NotFound();

            _context.Activities.Remove(activity);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
