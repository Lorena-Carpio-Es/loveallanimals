using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Repositories;
using Microsoft.AspNetCore.RateLimiting;

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
[EnableRateLimiting("PublicPolicy")]
public async Task<IActionResult> GetAll()
{
    var donations = await _service.GetAllAsync();

    var response = donations.Select(d => new DonationResponseDto
    {
        Id = d.Id,
        Amount = d.Amount,
        Date = d.Date,
        Status = d.Status.ToString(),
        UserId = d.UserId,
        UserName = d.User != null ? d.User.Name : "",
        CampaignId = d.CampaignId,
        CampaignTitle = d.Campaign != null ? d.Campaign.Title : ""
    });

    return Ok(response);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(long id)
{
    var d = await _service.GetByIdAsync(id);

    if (d == null)
        return NotFound();

    var response = new DonationResponseDto
    {
        Id = d.Id,
        Amount = d.Amount,
        Date = d.Date,
        Status = d.Status.ToString(),
        UserId = d.UserId,
        UserName = d.User != null ? d.User.Name : "",
        CampaignId = d.CampaignId,
        CampaignTitle = d.Campaign != null ? d.Campaign.Title : ""
    };

    return Ok(response);
}

[HttpGet("campaign/{campaignId}")]
[EnableRateLimiting("PublicPolicy")]
public async Task<IActionResult> GetByCampaign(long campaignId)
{
    var donations = await _service.GetByCampaignAsync(campaignId);

    var response = donations.Select(d => new DonationResponseDto
    {
        Id = d.Id,
        Amount = d.Amount,
        Date = d.Date,
        Status = d.Status.ToString(),
        UserId = d.UserId,
        UserName = d.User != null ? d.User.Name : "",
        CampaignId = d.CampaignId,
        CampaignTitle = d.Campaign != null ? d.Campaign.Title : ""
    });

    return Ok(response);
}

[HttpGet("user/{userId}")]
[EnableRateLimiting("PublicPolicy")]
public async Task<IActionResult> GetByUser(long userId)
{
    var donations = await _service.GetByUserAsync(userId);

    var response = donations.Select(d => new DonationResponseDto
    {
        Id = d.Id,
        Amount = d.Amount,
        Date = d.Date,
        Status = d.Status.ToString(),
        UserId = d.UserId,
        UserName = d.User != null ? d.User.Name : "",
        CampaignId = d.CampaignId,
        CampaignTitle = d.Campaign != null ? d.Campaign.Title : ""
    });

    return Ok(response);
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