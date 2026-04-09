using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/post")]
public class PublicacionController : ControllerBase
{
    private readonly IPostService _service;

    public PublicacionController(IPostService service)
    {
        _service = service;
    }

    // GET todas
    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    // GET por ID
    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var pub = _service.GetById(id);
        if (pub == null) return NotFound();
        return Ok(pub);
    }

    // GET por usuario
    [HttpGet("user/{id}")]
    public IActionResult GetByUser(long id)
        => Ok(_service.GetByUser(id));

    // POST crear
    [HttpPost]
    public IActionResult Create(CreatePostDto dto)
    {
        var pub = new Post
        {
            Title = dto.Title,
            FundraisingGoal = dto.FundraisingGoal,
            Description = dto.Description,
            Image = dto.Image,
            UserId = dto.UserId,
            CampaignId = dto.CampaignId
        };

        var created = _service.Create(pub);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT actualizar
    [HttpPut("{id}")]
    public IActionResult Update(long id, UpdatePostDto dto)
    {
        var pub = new Post
        {
            Title = dto.Title,
            Description = dto.Description
        };

        var updated = _service.Update(id, pub);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        if (!_service.Delete(id)) return NotFound();
        return NoContent();
    }

    // LIKE
    [HttpPost("{id}/likes")]
    public IActionResult Like(long id)
    {
        _service.DarLike(id);
        return Ok();
    }

    // SHARE
    [HttpPost("{id}/shares")]
    public IActionResult Share(long id)
    {
        _service.Compartir(id);
        return Ok();
    }
}