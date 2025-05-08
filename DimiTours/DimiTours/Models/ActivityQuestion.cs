namespace DimiTours.Models
{
    public class ActivityQuestion
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
