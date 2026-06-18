using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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

    [HttpGet]
    [EnableRateLimiting("PublicPolicy")]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _service.GetAllAsync();

        var response = posts.Select(p => new PostResponseDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            FundraisingGoal = p.FundraisingGoal,
            Image = p.Image,
            CreationDate = p.CreationDate,
            State = p.State.ToString(),
            QuantityLikes = p.QuantityLikes,
            QuantityShared = p.QuantityShared,
            QuantityComments = p.Comments != null ? p.Comments.Count : 0,
            UserId = p.UserId,
            UserName = p.User != null ? p.User.Name : "",
            CampaignId = p.CampaignId,
            CampaignTitle = p.Campaign != null ? p.Campaign.Title : ""
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    [EnableRateLimiting("PublicPolicy")]
    public async Task<IActionResult> GetById(long id)
    {
        var p = await _service.GetByIdAsync(id);

        if (p == null)
            return NotFound();

        var response = new PostResponseDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            FundraisingGoal = p.FundraisingGoal,
            Image = p.Image,
            CreationDate = p.CreationDate,
            State = p.State.ToString(),
            QuantityLikes = p.QuantityLikes,
            QuantityShared = p.QuantityShared,
            QuantityComments = p.Comments != null ? p.Comments.Count : 0,
            UserId = p.UserId,
            UserName = p.User != null ? p.User.Name : "",
            CampaignId = p.CampaignId,
            CampaignTitle = p.Campaign != null ? p.Campaign.Title : ""
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {
        try
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

            return Ok(new PostResponseDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                FundraisingGoal = created.FundraisingGoal,
                Image = created.Image,
                CreationDate = created.CreationDate,
                State = created.State.ToString(),
                QuantityLikes = created.QuantityLikes,
                QuantityShared = created.QuantityShared,
                QuantityComments = 0,
                UserId = created.UserId,
                UserName = "",
                CampaignId = created.CampaignId,
                CampaignTitle = ""
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdatePostDto dto)
    {
        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description
        };

        var ok = await _service.UpdateAsync(id, post);

        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);

        return ok ? NoContent() : NotFound();
    }

    [HttpPost("{id}/likes")]
    public async Task<IActionResult> Like(long id)
    {
        await _service.LikeAsync(id);

        return Ok("Like agregado");
    }

    [HttpPost("{id}/shares")]
    public async Task<IActionResult> Share(long id)
    {
        await _service.ShareAsync(id);

        return Ok("Publicación compartida");
    }
}