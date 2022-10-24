using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Models.Auth;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* API Controller for Auth
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) =>
            _authService = authService;

        // User Login
        [HttpPost("login")]
        public IActionResult UserLogin(AuthenticateRequest request)
        {
            // Calling async function made for validate user
            var response = _authService.UserLogin(request);

            return Ok(response);
        }

        // User Register
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