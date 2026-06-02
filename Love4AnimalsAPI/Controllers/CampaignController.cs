using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;

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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var campaign = await _service.GetByIdAsync(id);

        return campaign == null ? NotFound() : Ok(campaign);
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