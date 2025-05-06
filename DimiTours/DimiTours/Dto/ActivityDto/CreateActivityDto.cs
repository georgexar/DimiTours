using DimiTours.Dto.QuestionDto;

namespace DimiTours.Dto.Activity
{
    public class CreateActivityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public List<int> QuestionIds { get; set; } = new();
    }
}
