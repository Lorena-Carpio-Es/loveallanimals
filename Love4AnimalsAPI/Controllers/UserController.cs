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

    // GET
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _service.GetById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // POST
    [HttpPost]
    public IActionResult Create(CreateUserDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name
        };

        var created = _service.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateUserDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name
        };

        var updated = _service.Update(id, user);

        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);

        if (!deleted) return NotFound();

        return NoContent();
    }
}