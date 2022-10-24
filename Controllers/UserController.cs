/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

/*
* API Controller for User
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Defined User Service
        private readonly UserService _userService;

        // Constructor
        public UserController(UserService userService) =>
            _userService = userService;

        /**
         * Get All Users
         * GET: api/User
         *
         * @return Task<List<User>>
         * @see #GetUsers()
         */
        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetAsync();

        /**
         * Get User By Id
         * GET: api/User/{id}
         *
         * @return Task<ActionResult<User>>
         * @see #GetUserById(string id)
         */
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

        /**
         * Update User
         * PUT: api/User/{id}
         *
         * @return Task<IActionResult>
         * @see #UpdateUser(string id, UserUpdateDto userDto)
         */
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

        /**
         * Delete User
         * DELETE: api/User/{id}
         *
         * @return Task<IActionResult>
         * @see #DeleteUser(string id)
         */
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

        /**
         * Get User By Role
         * GET: api/User/role/{role}
         *
         * @return Task<ActionResult<List<User>>>
         * @see #GetUsersByRole(string role)
         */
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