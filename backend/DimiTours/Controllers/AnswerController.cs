using DimiTours.Data;
using DimiTours.Dto.AnswerDto;
using DimiTours.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/answer")]
    public class AnswerController : Controller
    {
        private readonly DataContext _context;

        public AnswerController(DataContext context)
        {
            _context = context;
        }

        // GET: api/v1/answer/all
        [HttpGet("all")]
        public IActionResult GetAllAnswers()
        {
            var answers = _context.Answers
                .Select(a => new AnswerResponseDto
                {
                    Id = a.Id,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect,
                    QuestionId = a.QuestionId
                })
                .ToList();

            return Ok(answers);
        }

        // GET: api/v1/answer/by-question/{id}
        [HttpGet("question/{id}")]
        public IActionResult GetAnswersForQuestion(int id)
        {
            var answers = _context.Answers
                .Where(a => a.QuestionId == id)
                .Select(a => new AnswerResponseDto
                {
                    Id = a.Id,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect,
                    QuestionId = a.QuestionId
                })
                .ToList();

            return Ok(answers);
        }

        // GET: api/v1/answer/correct
        [HttpGet("correct/all")]
        public IActionResult GetCorrectAnswersForAllQuestions()
        {
            var correctAnswers = _context.Answers
                .Where(a => a.IsCorrect)
                .Select(a => new AnswerResponseDto
                {
                    Id = a.Id,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect,
                    QuestionId = a.QuestionId
                })
                .ToList();

            return Ok(correctAnswers);
        }

        // GET: api/v1/answer/correct/{id}
        [HttpGet("correct/question/{id}")]
        public IActionResult GetCorrectAnswerForQuestion(int id)
        {
            var correctAnswer = _context.Answers
                .Where(a => a.QuestionId == id && a.IsCorrect)
                .Select(a => new AnswerResponseDto
                {
                    Id = a.Id,
                    Text = a.Text,
                    IsCorrect = a.IsCorrect,
                    QuestionId = a.QuestionId
                })
                .ToList();

            return Ok(correctAnswer);
        }

        [HttpPost("submit")]
        [Authorize]
        public IActionResult SubmitAnswer([FromBody] SubmitAnswerDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            List<Answer> answer = _context.Answers.Where(a => dto.AnswerId.Contains(a.Id)).ToList();

            if (answer.Count < 10)
            {
                return BadRequest("Something went wrong.");
            }

            foreach (Answer a in answer)
            {
                var userAnswer = new UserAnswer
                {
                    UserId = userId,
                    AnswerId = a.Id,
                    IsCorrect = a.IsCorrect
                };
                _context.UserAnswers.Add(userAnswer);
            }

            _context.SaveChanges();

            return Ok(new
            {
                message = "Answer submitted.",

            });
        }
    }
}
