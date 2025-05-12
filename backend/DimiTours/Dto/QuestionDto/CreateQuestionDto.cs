using DimiTours.Dto.AnswerDto;
using DimiTours.Enum;

namespace DimiTours.Dto.QuestionDto
{
    public class CreateQuestionDto
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<CreateAnswerDto> Answers { get; set; }
    }
}
