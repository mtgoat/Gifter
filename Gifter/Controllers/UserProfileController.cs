using System;
using Microsoft.AspNetCore.Mvc;
using Gifter.Repositories;
using Gifter.Models;

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
    }
}
