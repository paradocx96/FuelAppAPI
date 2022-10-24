using System.ComponentModel.DataAnnotations;

/*
 * EAD - FuelMe APP API
 *
 * Model class for Auth Request (Login)
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

namespace FuelAppAPI.Models.Auth
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}