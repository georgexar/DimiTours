using DimiTours.Dto.AnswerDto;
using DimiTours.Enum;

namespace DimiTours.Dto.QuestionDto
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public List<CreateAnswerDto> Answers { get; set; }
    }
}
