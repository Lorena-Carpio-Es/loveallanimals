using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;
using Microsoft.AspNetCore.RateLimiting;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/campaigns")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignService _service;

    public CampaignController(ICampaignService service)
    {
        _service = service;
    }

   [HttpGet]
[EnableRateLimiting("PublicPolicy")]
public async Task<IActionResult> GetAll()
{
    var campaigns = await _service.GetAllAsync();

    var response = campaigns.Select(c => new CampaignResponseDto
    {
        Id = c.Id,
        Title = c.Title,
        GoalAmount = c.GoalAmount,
        CurrentAmount = c.CurrentAmount,
        Status = c.Status.ToString(),
        Description = c.Description,
        TotalPosts = c.Posts != null ? c.Posts.Count : 0,
        TotalDonations = c.Donations != null ? c.Donations.Count : 0
    });

    return Ok(response);
}

[HttpGet("{id}")]
[EnableRateLimiting("PublicPolicy")]
public async Task<IActionResult> GetById(long id)
{
    var c = await _service.GetByIdAsync(id);

    if (c == null)
        return NotFound();

    var response = new CampaignResponseDto
    {
        Id = c.Id,
        Title = c.Title,
        GoalAmount = c.GoalAmount,
        CurrentAmount = c.CurrentAmount,
        Status = c.Status.ToString(),
        Description = c.Description,
        TotalPosts = c.Posts != null ? c.Posts.Count : 0,
        TotalDonations = c.Donations != null ? c.Donations.Count : 0
    };

    return Ok(response);
}

    [HttpPost]
    public async Task<IActionResult> Create(CreateCampaignDto dto)
    {
        var campaign = new Campaign
        {
            Title = dto.Title,
            GoalAmount = dto.GoalAmount,
            Description = dto.Description
        };

        return Ok(await _service.CreateAsync(campaign));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateCampaignDto dto)
    {
        var campaign = new Campaign
        {
            Title = dto.Title,
            GoalAmount = dto.GoalAmount,
            CurrentAmount = dto.CurrentAmount,
            Status = dto.Status,
            Description = dto.Description
        };

        var ok = await _service.UpdateAsync(id, campaign);

        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);

        return ok ? NoContent() : NotFound();
    }
}