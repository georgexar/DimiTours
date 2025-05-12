using DimiTours.Dto.SectionDto;
using DimiTours.Models;

namespace DimiTours.Dto.Section
{
    public class SectionResponseDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } 
        public string Title { get; set; }
        public string Purpose { get; set; }
        public string ContentText { get; set; }
        public List<MediaItemResponseDto>? MediaItems { get; set; }
    }
}
