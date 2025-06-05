﻿using DimiTours.Enum;

namespace DimiTours.Dto.AuthDto
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }
}
