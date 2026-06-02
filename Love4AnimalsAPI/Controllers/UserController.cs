using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/users/profile")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllAsync();

        var response = users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

        return Ok(response);
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        try
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            var created = await _service.RegisterAsync(user, dto.Password);

            var response = new UserResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var user = await _service.LoginAsync(dto.Email, dto.Password);

        if (user == null)
            return Unauthorized("Email o contraseña incorrectos");

        return Ok(new
        {
            message = "Login exitoso",
            user = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            }
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email
        };

        var ok = await _service.UpdateAsync(id, user);

        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);

        return ok ? NoContent() : NotFound();
    }
}