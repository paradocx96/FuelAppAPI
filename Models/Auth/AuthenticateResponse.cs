namespace FuelAppAPI.Models.Auth;

public class AuthenticateResponse
{
    public string? Id { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}