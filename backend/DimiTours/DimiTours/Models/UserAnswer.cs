using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DimiTours.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
