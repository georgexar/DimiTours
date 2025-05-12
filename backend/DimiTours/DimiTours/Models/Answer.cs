using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DimiTours.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        [JsonIgnore]
        public Question Question { get; set; }
    }
}
