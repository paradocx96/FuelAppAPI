using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* API Controller for User
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        // Get All Users
        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetAsync();

        // Get User By Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            // Calling async function made for get user by user id
            var user = await _userService.GetAsync(id);

            // Checking user availability
            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // Update User
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, UserUpdateDto userDto)
        {
            // Calling async function made for get user by user id
            var userCheck = await _userService.GetAsync(id);

            // Checking user availability
            if (userCheck is null)
            {
                return NotFound();
            }

            // Create new user object and assign values for update user
            User updatedUser = new User();
            updatedUser.Id = id;
            updatedUser.Username = userCheck.Username;
            updatedUser.FullName = userDto.FullName;
            updatedUser.Email = userDto.Email;
            updatedUser.Password = userCheck.Password;
            updatedUser.Role = userCheck.Role;

            // Calling async function made for update user
            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        // Delete User
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Calling async function made for get user by user id
            var user = await _userService.GetAsync(id);

            // Checking user availability
            if (user is null)
            {
                return NotFound();
            }

            // Calling async function made for delete user by user id
            await _userService.RemoveAsync(id);

            return NoContent();
        }
        
        // Get Users By Role
        [HttpGet("role/{role}")]
        public async Task<ActionResult<List<User>>> GetUsersByRole(string role)
        {
            // Calling async function made for get users by role
            var users = _userService.GetUsersByRole(role);

            // Checking user availability
            if (users.Count == 0)
            {
                return NotFound();
            }

            return users;
        }
    }
}