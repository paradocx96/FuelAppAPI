/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Data Transfer Object for User
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