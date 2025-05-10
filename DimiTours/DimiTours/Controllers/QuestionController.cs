using DimiTours.Data;
using DimiTours.Dto.QuestionDto;
using DimiTours.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/question")]
    public class QuestionController : Controller
    {
        private readonly DataContext _context;

        public QuestionController(DataContext context)
        {
            _context = context;
        }

        // POST: api/v1/question
        [HttpPost("create")]
        public IActionResult CreateQuestion([FromBody] CreateQuestionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = new Question
            {
                Text = dto.Text,
                Type = dto.Type,
                Answers = new List<Answer>()
            };

            foreach (var answerDto in dto.Answers)
            {
                question.Answers.Add(new Answer
                {
                    Text = answerDto.Text,
                    IsCorrect = answerDto.IsCorrect
                });
            }

            _context.Questions.Add(question);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        }

        // GET: api/v1/question/{id}
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // GET: api/v1/question/all
        [HttpGet("all")]
        public IActionResult GetAllQuestions()
        {
            var questions = _context.Questions
                .Include(q => q.Answers)
                .ToList();

            return Ok(questions);
        }

        // DELETE: api/v1/question/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
