namespace Love4AnimalsAPI.Dto;

public class AuthResponseDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime ExpiresAt { get; set; }

    public UserResponseDto User { get; set; }
}