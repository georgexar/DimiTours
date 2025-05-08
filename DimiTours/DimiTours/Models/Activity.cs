using DimiTours.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DimiTours.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        // Navigation property για Many-to-Many
        public ICollection<ActivityQuestion> ActivityQuestions { get; set; } = new List<ActivityQuestion>();



    }
}
