using DimiTours.Data;
using DimiTours.Dto.AdaptiveLearning;
using DimiTours.Dto.UserDto;
using DimiTours.Enum;
using DimiTours.Interfaces;
using DimiTours.Models;
using Microsoft.EntityFrameworkCore;

namespace DimiTours.Services
{
    public class UserProgressService : IUserProgressService
    {
        private readonly DataContext _context;

        public UserProgressService(DataContext context)
        {
            _context = context;
        }

        public List<UserAnswerDto> GetAllAnswers(int userId)
        {
            var allAnswers = _context.UserAnswers
                .Include(u => u.Answer)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .AsEnumerable() // ← Forces switch to client-side evaluation
                .Select(a => new UserAnswerDto { UserAnswer = a })
                .ToList();

            return allAnswers;
        }

        public string GetOverallSuccessRate(int userId)
        {
            var lastAnswers = _context.UserAnswers
                .Include(u => u.Answer)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();

            if (!lastAnswers.Any())
                return "0/0 (0%)";

            int total = lastAnswers.Count;
            int correct = lastAnswers.Count(ua => ua.IsCorrect);
            double percentage = (double)correct / total * 100;

            return $"{correct}/{total} ({Math.Round(percentage, 2)}%)";
        }

        public List<QuestionTypeSuccessRateDto> GetSuccessRatePerQuestionType(int userId)
        {
            var lastAnswers = _context.UserAnswers
                .Include(ua => ua.Answer)
                    .ThenInclude(a => a.Question)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();

            var allQuestionTypes = ((QuestionType[])System.Enum.GetValues(typeof(QuestionType))).ToList();
            var result = new List<QuestionTypeSuccessRateDto>();

            foreach (var type in allQuestionTypes)
            {
                var answersOfType = lastAnswers.Where(ua => ua.Answer.Question.Type == type).ToList();

                int total = answersOfType.Count;
                int correct = answersOfType.Count(ua => ua.IsCorrect);
                double percentage = total > 0 ? (double)correct / total * 100 : 0;

                result.Add(new QuestionTypeSuccessRateDto
                {
                    QuestionType = type.ToString(),
                    SuccessRate = $"{correct}/{total} ({Math.Round(percentage, 2)}%)"
                });
            }

            return result;
        }

        public string GetSuccessRateInActivity(int userId, int activityId)
        {
            var activity = _context.Activities
                .Include(a => a.ActivityQuestions)
                .ThenInclude(aq => aq.Question)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(a => a.Id == activityId);

            if (activity == null)
                return "Activity not found";

            var questionIds = activity.ActivityQuestions.Select(aq => aq.QuestionId).ToList();

            var lastAnswers = _context.UserAnswers
                .Include(ua => ua.Answer)
                .Where(ua => ua.UserId == userId && questionIds.Contains(ua.Answer.QuestionId))
                .GroupBy(ua => ua.Answer.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();

            if (!lastAnswers.Any())
                return "0/0 (0%)";

            int total = lastAnswers.Count;
            int correct = lastAnswers.Count(ua => ua.IsCorrect);
            double percentage = (double)correct / total * 100;

            return $"{correct}/{total} ({Math.Round(percentage, 2)}%)";
        }
    }
}
