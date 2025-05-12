using DimiTours.Dto.AnswerDto;
using DimiTours.Enum;

namespace DimiTours.Dto.QuestionDto
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }

        public List<AnswerResponseDto> Answers { get; set; }
    }
}
