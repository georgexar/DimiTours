using DimiTours.Data;
using DimiTours.Dto.AdaptiveLearning;
using DimiTours.Enum;
using DimiTours.Interfaces;
using DimiTours.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DimiTours.Services
{
    // This service provides adaptive question recommendations for a user based on past performance
    public class AdaptiveContentService : IAdaptiveContentService
    {
        private readonly DataContext _context;
        private const int TotalQuestionsToRecommend = 10;

        // Constructor that injects the application's database context
        public AdaptiveContentService(DataContext context)
        {
            _context = context;

        }

        // Main method to get personalized recommendations based on user's answer history
        public AdaptiveContentRecommendationDto GetAdaptiveRecommendations(int userId)
        {
            var result = new AdaptiveContentRecommendationDto();

            // Get the last answer the user gave for each question
            var lastAnswers = _context.UserAnswers
                .Include(ua => ua.Answer)
                    .ThenInclude(a => a.Question)
                .Where(ua => ua.UserId == userId)
                .GroupBy(ua => ua.Answer.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.CreatedAt).First())
                .ToList();

            // If the user has no answers, return an empty result
            if (!lastAnswers.Any())
                return result;

            // Calculate success rate per question type
            var groupedByType = lastAnswers
                .GroupBy(ua => ua.Answer.Question.Type)
                .ToDictionary(g => g.Key, g =>
                {
                    int total = g.Count();
                    int correct = g.Count(ua => ua.IsCorrect);
                    return new { Total = total, Correct = correct };
                });

            // Get all possible question types
            var allTypes = ((QuestionType[])System.Enum.GetValues(typeof(QuestionType))).ToList();

            // Initialize weights for each type (1 - success rate)
            var weights = new Dictionary<QuestionType, double>();
            foreach (var type in allTypes)
            {
                if (groupedByType.ContainsKey(type))
                {
                    var stats = groupedByType[type];
                    double successRate = stats.Total > 0 ? (double)stats.Correct / stats.Total : 0;
                    weights[type] = 1 - successRate;
                }
                else
                {
                    weights[type] = 1; // No experience with this type -> highest priority
                }
            }
            // Normalize weights so they sum to 1
            double totalWeight = weights.Values.Sum();
            if (totalWeight == 0)
                totalWeight = 1;


            var normalizedWeights = weights.ToDictionary(kv => kv.Key, kv => kv.Value / totalWeight);

            // Determine how many questions to recommend per type
            var typeToCount = normalizedWeights.ToDictionary(
                kv => kv.Key,
                kv => (int)Math.Round(kv.Value * TotalQuestionsToRecommend)
            );

            // Adjust total count to ensure it equals the total desired (20)
            int currentTotal = typeToCount.Values.Sum();
            while (currentTotal < TotalQuestionsToRecommend)
            {
                var maxType = normalizedWeights.OrderByDescending(kv => kv.Value).First().Key;
                typeToCount[maxType]++;
                currentTotal++;
            }
            while (currentTotal > TotalQuestionsToRecommend)
            {
                var minType = typeToCount.Where(kv => kv.Value > 0).OrderBy(kv => kv.Value).First().Key;
                typeToCount[minType]--;
                currentTotal--;
            }

            // Fetch actual question objects for each type
            var weakQuestions = new List<Question>();

            foreach (var kv in typeToCount)
            {
                var type = kv.Key;
                var count = kv.Value;

                // Get all questions of this type
                var candidateQuestions = _context.Questions
                    .Include(q => q.Answers)
                    .Where(q => q.Type == type)
                    .ToList();

                // Select questions the user got wrong or hasn't answered
                var unansweredOrWrong = candidateQuestions
                    .Where(q => !lastAnswers.Any(ua => ua.Answer.QuestionId == q.Id && ua.IsCorrect))
                    .OrderBy(q => Guid.NewGuid())
                    .Take(count)
                    .ToList();

                weakQuestions.AddRange(unansweredOrWrong);
            }







            if (!weakQuestions.Any())
            {
                // Επέλεξε 10 random ερωτήσεις για εξάσκηση
                weakQuestions = _context.Questions
                    .OrderBy(q => Guid.NewGuid())
                    .Take(TotalQuestionsToRecommend)
                    .ToList();

                // Βάλε μήνυμα στο sections
                result.RecommendedSections.Add("Είσαι άριστος σε όλα! Ορίστε μερικές ερωτήσεις για εξάσκηση.");
            }
            else
            {
                var weakQuestionIds = weakQuestions.Select(q => q.Id).ToList();

                var activityIds = _context.ActivityQuestions
                    .Where(aq => weakQuestionIds.Contains(aq.QuestionId))
                    .Select(aq => aq.ActivityId)
                    .Distinct()
                    .ToList();

                var sectionTitles = _context.Activities
                    .Include(a => a.Section)
                    .Where(a => activityIds.Contains(a.Id))
                    .Select(a => a.Section.Title)
                    .Distinct()
                    .ToList();

                result.RecommendedSections = sectionTitles;
            }

            result.WeakQuestions = weakQuestions;
            return result;
        }
    }
}