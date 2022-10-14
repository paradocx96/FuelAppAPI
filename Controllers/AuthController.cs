using FuelAppAPI.Models;
using FuelAppAPI.Models.Auth;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
            var response = _authService.UserLogin(request);

            return Ok(response);
        }

        // User Register
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(User user)
        {
            var response = await _authService.UserRegistration(user);

            return Ok(response);
        }
    }
}