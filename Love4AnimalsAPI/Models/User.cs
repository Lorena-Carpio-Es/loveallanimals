namespace Love4AnimalsAPI.Models;

public record class User
{
    public long  Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public List<Post> Posts { get; set; } = new();

}
