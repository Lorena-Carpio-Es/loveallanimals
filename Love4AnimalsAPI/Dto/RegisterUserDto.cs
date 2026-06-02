using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Dto;

public class RegisterUserDto
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }
}