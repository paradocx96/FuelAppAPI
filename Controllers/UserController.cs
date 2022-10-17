﻿using FuelAppAPI.Models;
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
        
        // Get All Users
        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetAsync();

        // Get User By Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUserById(string id)
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
        
        // Delete User
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetAsync(id);
        
            if (user is null)
            {
                return NotFound();
            }
        
            await _userService.RemoveAsync(id);
        
            return NoContent();
        }
    }
}
