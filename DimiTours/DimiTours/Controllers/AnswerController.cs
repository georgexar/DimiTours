using DimiTours.Data;
using DimiTours.Dto.AnswerDto;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("by-question/{id}")]
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
        [HttpGet("correct")]
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
        [HttpGet("correct/{id}")]
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
    }
}
