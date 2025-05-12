using DimiTours.Dto.QuestionDto;
using DimiTours.Dto.Section;

namespace DimiTours.Dto.Activity
{
    public class ActivityResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SectionResponseDto Section { get; set; }
        public List<QuestionResponseDto> Questions { get; set; }
    }
}
