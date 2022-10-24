/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Data Transfer Object for User
*/

namespace FuelAppAPI.DTO
{
    public class UserUpdateDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}