using System.ComponentModel.DataAnnotations;

namespace DimiTours.Models
{
    public class Section
    {

        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string ContentText { get; set; }
        public List<MediaItem>? MediaItems { get; set; }

    }
}
