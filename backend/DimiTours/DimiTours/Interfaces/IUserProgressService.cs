using DimiTours.Dto.AdaptiveLearning;
using DimiTours.Dto.UserDto;

namespace DimiTours.Interfaces
{
    public interface IUserProgressService
    {
        List<UserAnswerDto> GetAllAnswers(int userId);
        string GetOverallSuccessRate(int userId);
        List<QuestionTypeSuccessRateDto> GetSuccessRatePerQuestionType(int userId);
        string GetSuccessRateInActivity(int userId, int activityId);
    }
}
