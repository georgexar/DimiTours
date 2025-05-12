using DimiTours.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DimiTours.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        [Required]
        public List<Answer> Answers { get; set; }
        public List<ActivityQuestion> ActivityQuestions { get; set; }


    }
}
