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
    public async Task<IActionResult> Create([FromForm] CreatePostDto dto)
    {
    string imageUrl = "";

    if (dto.Image != null)
    {
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await dto.Image.CopyToAsync(stream);

        imageUrl = $"http://localhost:5116/images/{fileName}";
    }

    var post = new Post
    {
        Title = dto.Title,
        FundraisingGoal = dto.FundraisingGoal,
        Description = dto.Description,
        Image = imageUrl,
        UserId = dto.UserId,           // 🔥 IMPORTANTE
        CampaignId = dto.CampaignId    // 🔥 IMPORTANTE
    };

    var created = await _service.CreateAsync(post);

    return Ok(created);
   }
}