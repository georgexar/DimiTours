using DimiTours.Data;
using DimiTours.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
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
    }
}
