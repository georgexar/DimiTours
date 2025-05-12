using DimiTours.Data;
using DimiTours.Dto.Activity;
using DimiTours.Dto.AnswerDto;
using DimiTours.Dto.QuestionDto;
using DimiTours.Dto.Section;
using DimiTours.Dto.SectionDto;
using DimiTours.Interfaces;
using DimiTours.Models;
using DimiTours.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/activity")]
    public class ActivityController : Controller
    {
        private readonly DataContext _context;
        private readonly IAdaptiveContentService _adaptiveContentService;
        public ActivityController(DataContext context, IAdaptiveContentService adaptiveContentService)
        {
            _context = context;
            _adaptiveContentService = adaptiveContentService;

        }

        // GET ALL
        [HttpGet("all")]
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
                    .ThenInclude(s => s.MediaItems)
                .Include(a => a.ActivityQuestions)
                    .ThenInclude(aq => aq.Question)
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
                    MediaItems = activity.Section.MediaItems?.Select(m => new MediaItemResponseDto
                    {
                        MediaType = m.MediaType,
                        Url = m.Url
                    }).ToList()
                },
                Questions = activity.ActivityQuestions.Select(aq => new QuestionResponseDto
                {
                    Id = aq.Question.Id,
                    Text = aq.Question.Text,
                    Type = aq.Question.Type,
                    Answers = aq.Question.Answers.Select(ans => new AnswerResponseDto
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
                    .ThenInclude(s => s.MediaItems)
                 .Include(a => a.ActivityQuestions)
                     .ThenInclude(aq => aq.Question)
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
                    MediaItems = activity.Section.MediaItems?.Select(m => new MediaItemResponseDto
                    {
                        MediaType = m.MediaType,
                        Url = m.Url
                    }).ToList()
                },
                Questions = activity.ActivityQuestions.Select(aq => new QuestionResponseDto
                {
                    Id = aq.Question.Id,
                    Text = aq.Question.Text,
                    Type = aq.Question.Type,
                    Answers = aq.Question.Answers.Select(ans => new AnswerResponseDto
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
        [HttpGet("section/{sectionId}")]
        public IActionResult GetActivitiesBySectionId(int sectionId)
        {
            var activities = _context.Activities
                .Include(a => a.Section)
                    .ThenInclude(s => s.MediaItems)
                .Include(a => a.ActivityQuestions)
                    .ThenInclude(aq => aq.Question)
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
                    MediaItems = a.Section.MediaItems?.Select(m => new MediaItemResponseDto
                    {
                        MediaType = m.MediaType,
                        Url = m.Url
                    }).ToList()
                },
                Questions = a.ActivityQuestions.Select(aq => new QuestionResponseDto
                {
                    Id = aq.Question.Id,
                    Text = aq.Question.Text,
                    Type = aq.Question.Type,
                    Answers = aq.Question.Answers.Select(ans => new AnswerResponseDto
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
        [HttpPost("create")]
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
                ActivityQuestions = questions.Select(q => new ActivityQuestion
                {
                    QuestionId = q.Id
                }).ToList()
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, null);
        }

        // DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _context.Activities
               .Include(a => a.ActivityQuestions)
               .FirstOrDefault(a => a.Id == id);

            if (activity == null) return NotFound();

            _context.ActivityQuestions.RemoveRange(activity.ActivityQuestions);
            _context.Activities.Remove(activity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpGet("adaptive/recommendations")]
        [Authorize]
        public IActionResult GetAdaptiveRecommendations()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }
         
            var recommendations = _adaptiveContentService.GetAdaptiveRecommendations(userId);

            if (recommendations == null)
                return NotFound("No recommendations available.");

            return Ok(recommendations);
        }


    }
}
