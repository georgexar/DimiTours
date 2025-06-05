using DimiTours.Dto.SectionDto;
using DimiTours.Models;
using System.ComponentModel.DataAnnotations;

namespace DimiTours.Dto.Section
{
    public class CreateSectionDto
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Purpose { get; set; }
        public string ContentText { get; set; }
        public List<CreateMediaItemDto>? MediaItems { get; set; }
    }
}
