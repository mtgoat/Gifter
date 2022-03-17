using System;
using Microsoft.AspNetCore.Mvc;
using Gifter.Models;
using Gifter.Repositories;
using Microsoft.AspNetCore.Http;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }


        [HttpGet("user/{userId}")]
        public IActionResult GetAllPostsByUserId(int userId)
        {
            return Ok(_userProfileRepository.GetAllPostsByUserId(userId));
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userProfileRepository.GetByEmail(email);
            if (email == null || user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(UserProfile user)
        {
            _userProfileRepository.Add(user);
            return CreatedAtAction("GetByEmail", new { email = user.Email }, user);
        }
    }
}
