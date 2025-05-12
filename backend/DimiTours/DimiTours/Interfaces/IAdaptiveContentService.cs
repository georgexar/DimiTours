using DimiTours.Dto.AdaptiveLearning;
using DimiTours.Models;

namespace DimiTours.Interfaces
{
    public interface IAdaptiveContentService
    {
        AdaptiveContentRecommendationDto GetAdaptiveRecommendations(int userId);

        
    }
}
