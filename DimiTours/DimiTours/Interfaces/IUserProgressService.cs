using DimiTours.Dto.AdaptiveLearning;

namespace DimiTours.Interfaces
{
    public interface IUserProgressService
    {
        string GetOverallSuccessRate(int userId);
        List<QuestionTypeSuccessRateDto> GetSuccessRatePerQuestionType(int userId);
        string GetSuccessRateInActivity(int userId, int activityId);
    }
}
