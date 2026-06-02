using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Repositories;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var post = await _service.GetByIdAsync(id);
        return post == null ? NotFound() : Ok(post);
    }

    [HttpPost]
public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
{
    var post = new Post
    {
        Title = dto.Title,
        FundraisingGoal = dto.FundraisingGoal,
        Description = dto.Description,
        Image = dto.Image,
        UserId = dto.UserId,
        CampaignId = dto.CampaignId
    };

    var created = await _service.CreateAsync(post);

    return Ok(created);
}
}