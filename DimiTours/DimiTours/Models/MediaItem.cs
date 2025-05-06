using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DimiTours.Enum;

namespace DimiTours.Models
{
    public class MediaItem
    {
        public int Id { get; set; }

        [Required]
        public MediaType MediaType { get; set; }

        [Required]
        public string Url { get; set; }

    }
}
