using DimiTours.Data;
using DimiTours.Dto.UserDto;
using DimiTours.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Linq;
using DimiTours.Interfaces;
using DimiTours.Services;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserProgressService _userProgressService;
        private readonly DataContext _context;

        public UserController(DataContext context, IUserProgressService userProgressService)
        {
            _context = context;
            _userProgressService = userProgressService;
        }


        [HttpGet("me")]
        [Authorize]
        public IActionResult ViewMyProfile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            var userProfile = new UserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Gender = user.Gender
                
            };

            return Ok(userProfile);
            
        }

        [HttpGet("profile/answers")]
        [Authorize]
        public IActionResult GetAllAnswers()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = _userProgressService.GetAllAnswers(userId);
            return Ok(new { result });
        }

        [HttpGet("profile/overall/success")]
        [Authorize]
        public IActionResult GetUserOverallSuccessRate()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = _userProgressService.GetOverallSuccessRate(userId);
            return Ok(new { SuccessRate = result });
        }

        [HttpGet("profile/success/questiontype")]
        [Authorize]
        public IActionResult GetUserSuccessRatePerQuestionType()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = _userProgressService.GetSuccessRatePerQuestionType(userId);
            return Ok(result);
        }

        [HttpGet("activity/{id}/success")]
        [Authorize]
        public IActionResult GetUserSuccessRateInActivity(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = _userProgressService.GetSuccessRateInActivity(userId, id);

            if (result == "Activity not found")
                return NotFound(result);

            return Ok(new { SuccessRate = result });
        }



    }
}
