namespace DimiTours.Dto.AnswerDto
{
    public class AnswerResponseDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }       
    }
}
