namespace Love4AnimalsAPI.Models;

public class User
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public List<Post> Posts { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public List<Donation> Donations { get; set; } = new();
}