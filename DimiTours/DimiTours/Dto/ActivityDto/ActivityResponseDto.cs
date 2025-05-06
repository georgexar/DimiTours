using DimiTours.Dto.QuestionDto;

namespace DimiTours.Dto.Activity
{
    public class ActivityResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public List<QuestionResponseDto> Questions { get; set; }
    }
}
