using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        // Register User
        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {
            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetUserbyId), new { id = user.Id }, user);
        }

        // Get All Users
        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetAsync();

        // Get User By Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUserbyId(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // Update User
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }
    }
}
