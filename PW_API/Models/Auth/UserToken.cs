namespace PW_API.Models.Auth;

public class UserToken
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
}
