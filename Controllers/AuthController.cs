/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Auth;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

/*
* API Controller for Auth
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*
* Reference:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
* https://mongodb.github.io/mongo-csharp-driver/
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Defined Auth Service
        private readonly AuthService _authService;

        // Constructor
        public AuthController(AuthService authService) =>
            _authService = authService;

        /**
         * User Login
         * POST: api/Auth/login
         *
         * @return IActionResult
         * @see #UserLogin(AuthenticateRequest request)
         */
        [HttpPost("login")]
        public IActionResult UserLogin(AuthenticateRequest request)
        {
            // Calling async function made for validate user
            var response = _authService.UserLogin(request);

            return Ok(response);
        }


        /**
         * User Register
         * POST: api/Auth/register
         * 
         * @return Task<IActionResult>
         * @see #UserRegister(UserDto userDto)
         */
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserDto userDto)
        {
            // Create new User object
            User user = new User();
            user.Username = userDto.Username;
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.Role = userDto.Role;

            // Calling async function made for create user
            var response = await _authService.UserRegistration(user);

            return Ok(response);
        }
    }
}