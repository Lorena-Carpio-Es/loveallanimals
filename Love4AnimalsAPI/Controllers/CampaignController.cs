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
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var campaign = _service.GetById(id);

        if (campaign == null) return NotFound();

        return Ok(campaign);
    }

    [HttpPost]
    public IActionResult Create(CreateCampaignDto dto)
    {
        var campaign = new Campaign
        {
            Title = dto.Title,
            GoalAmount = dto.GoalAmount,
            Description = dto.Description
        };

        return Ok(_service.Create(campaign));
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateCampaignDto dto)
    {
        var campaign = new Campaign
        {
            Title = dto.Title,
            GoalAmount = dto.GoalAmount,
            CurrentAmount = dto.CurrentAmount,
            Status = dto.Status,
            Description = dto.Description
        };

        var updated = _service.Update(id, campaign);

        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);

        if (!deleted) return NotFound();

        return Ok("Campaign deleted");
    }
}