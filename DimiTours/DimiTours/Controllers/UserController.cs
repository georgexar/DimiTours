using DimiTours.Data;
using DimiTours.Dto.UserDto;
using DimiTours.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Linq;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("me")]
        [Authorize]
        public IActionResult ViewMyProfile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            var userProfile = new UserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Gender = user.Gender
                
            };

            return Ok(userProfile);
            
        }





        [HttpGet("profile/success")]
        [Authorize]
        public IActionResult GetUserOverallSuccessRate()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }


            var lastAnswers = _context.UserAnswers
                .Include(u => u.Answer)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId) // Παίρνουμε το QuestionId από το Answer
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();


            if (!lastAnswers.Any())
                return Ok(new { SuccessRate = "0/0 (0%)" });

            int total = lastAnswers.Count;
            int correct = lastAnswers.Count(ua => ua.IsCorrect);
            double percentage = (double)correct / total * 100;

            string result = $"{correct}/{total} ({Math.Round(percentage, 2)}%)";

            return Ok(new { SuccessRate = result });
        }


        [HttpGet("profile/success/questiontype")]
        [Authorize]
        public IActionResult GetUserSuccessRatePerQuestionType()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var lastAnswers = _context.UserAnswers
                .Include(ua => ua.Answer)  // Φορτώνουμε την απάντηση για να πάρουμε το Question
                    .ThenInclude(a => a.Question)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId) // Παίρνουμε το QuestionId από το Answer
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();

            // Χρησιμοποιούμε πλήρες όνομα για System.Enum ώστε να μην υπάρχει σύγκρουση
            var allQuestionTypes = ((QuestionType[])System.Enum.GetValues(typeof(QuestionType))).ToList();

            var result = new List<object>();

            foreach (var type in allQuestionTypes)
            {
                var answersOfType = lastAnswers.Where(ua => ua.Answer.Question.Type == type).ToList();

                int total = answersOfType.Count;
                int correct = answersOfType.Count(ua => ua.IsCorrect);
                double percentage = total > 0 ? (double)correct / total * 100 : 0;

                result.Add(new
                {
                    QuestionType = type.ToString(),
                    SuccessRate = $"{correct}/{total} ({Math.Round(percentage, 2)}%)"
                });
            }

            return Ok(result);
        }

        [HttpGet("activity/{id}/success")]
        [Authorize]
        public IActionResult GetUserSuccessRateInActivity(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
                return Unauthorized();

            var activity = _context.Activities
                .Include(a => a.ActivityQuestions)
                .ThenInclude(aq => aq.Question)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(a => a.Id == id);

            if (activity == null)
                return NotFound("Activity not found");

            var questionIds = activity.ActivityQuestions.Select(aq => aq.QuestionId).ToList();


            var lastAnswers = _context.UserAnswers
                    .Include(ua => ua.Answer)
                    .Where(ua => ua.UserId == userId && questionIds.Contains(ua.Answer.QuestionId)) // Παίρνουμε το QuestionId από το Answer
                    .GroupBy(ua => ua.Answer.QuestionId)
                    .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                    .ToList();

            if (!lastAnswers.Any())
                return Ok(new { SuccessRate = "0/0 (0%)" });

            int total = lastAnswers.Count;
            int correct = lastAnswers.Count(ua => ua.IsCorrect);
            double percentage = (double)correct / total * 100;

            string result = $"{correct}/{total} ({Math.Round(percentage, 2)}%)";

            return Ok(new { SuccessRate = result });
        }



    }
}
