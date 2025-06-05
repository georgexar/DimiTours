﻿using DimiTours.Data;
using DimiTours.Dto.AuthDto;
using DimiTours.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DimiTours.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(DataContext context, IConfiguration config, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _config = config;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            // Find User in Db
            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == request.Username);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials");
            }

            // Create claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            // create key and token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //return Ok(new { token = jwt });

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new { message = "Logged in successfully." });

        }


        // Register
        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDto request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == request.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already taken");
            }

            var user = new User
            {
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender
            };

            // Hash the password before saving
            user.Password = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Logged out successfully." });
        }

    }
}
