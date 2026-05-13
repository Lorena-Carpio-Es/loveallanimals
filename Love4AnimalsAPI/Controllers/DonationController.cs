using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/donations")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _service;

    public DonationController(IDonationService service)
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
        var donation = await _service.GetByIdAsync(id);

        return donation == null ? NotFound() : Ok(donation);
    }

    [HttpGet("campaign/{campaignId}")]
    public async Task<IActionResult> GetByCampaign(long campaignId)
    {
        return Ok(await _service.GetByCampaignAsync(campaignId));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(long userId)
    {
        return Ok(await _service.GetByUserAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDonationDto dto)
    {
        try
        {
            var donation = new Donation
            {
                Amount = dto.Amount,
                UserId = dto.UserId,
                CampaignId = dto.CampaignId
            };

            var created = await _service.CreateAsync(donation);

            return Ok(created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateDonationDto dto)
    {
        var donation = new Donation
        {
            Amount = dto.Amount,
            Status = dto.Status
        };

        var ok = await _service.UpdateAsync(id, donation);

        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var ok = await _service.DeleteAsync(id);

        return ok ? NoContent() : NotFound();
    }
}