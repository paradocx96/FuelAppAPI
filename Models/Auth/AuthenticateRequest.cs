using System.ComponentModel.DataAnnotations;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Model class for Auth Request (Login)
*/
namespace FuelAppAPI.Models.Auth
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}