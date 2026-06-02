using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
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
    [Authorize(Roles = "Misionero")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllAsync();

        var response = users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role.ToString()
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("PublicPolicy")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        try
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role
            };

            var created = await _service.RegisterAsync(user, dto.Password);

            return Ok(new UserResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                Role = created.Role.ToString()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("PublicPolicy")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var response = await _service.LoginAsync(dto.Email, dto.Password);

        if (response == null)
            return Unauthorized("Email o contraseña incorrectos");

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
    {
        var response = await _service.RefreshTokenAsync(dto.RefreshToken);

        if (response == null)
            return Unauthorized("Refresh token inválido o expirado");

        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize]
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
    [Authorize(Roles = "Misionero")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);

        return ok ? NoContent() : NotFound();
    }
}