/*
 * EAD - FuelMe APP API
 *
 * Data Transfer Object for User
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

namespace FuelAppAPI.DTO
{
    public class UserDto
    {
        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}