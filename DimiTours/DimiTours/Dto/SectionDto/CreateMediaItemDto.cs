using DimiTours.Enum;
using System.ComponentModel.DataAnnotations;

namespace DimiTours.Dto.SectionDto
{
    public class CreateMediaItemDto
    {
        [Required]
        public MediaType MediaType { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
