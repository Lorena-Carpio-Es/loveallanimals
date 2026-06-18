using System.Text.Json.Serialization;

namespace Love4AnimalsAPI.Models;

public class User
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }

    [JsonIgnore]
    public DateTime? RefreshTokenExpiryTime { get; set; }

    [JsonIgnore]
    public List<Post> Posts { get; set; } = new();

    [JsonIgnore]
    public List<Donation> Donations { get; set; } = new();
}