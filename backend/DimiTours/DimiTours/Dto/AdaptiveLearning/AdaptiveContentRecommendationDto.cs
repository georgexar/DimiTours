using DimiTours.Models;

namespace DimiTours.Dto.AdaptiveLearning
{
    public class AdaptiveContentRecommendationDto
    {
        public List<Question> WeakQuestions { get; set; } = new();
        public List<string> RecommendedSections { get; set; } = new();
    }
}
