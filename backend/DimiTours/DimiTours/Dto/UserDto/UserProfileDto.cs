using DimiTours.Enum;

namespace DimiTours.Dto.UserDto
{
    public class UserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
    }
}
