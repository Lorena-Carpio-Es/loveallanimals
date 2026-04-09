using Microsoft.AspNetCore.Mvc;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class DonationsController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationsController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    // GET /v1/campaigns/{id}/donations
    [HttpGet("campaigns/{campaignId}/donations")]
    public ActionResult<List<Donation>> GetByCampaign(int campaignId)
    {
        var donations = _donationService.GetByCampaignId(campaignId);
        return Ok(donations);
    }

    // POST /v1/campaigns/{id}/donations
    [HttpPost("campaigns/{campaignId}/donations")]
    public ActionResult<Donation> Create(int campaignId, [FromBody] Donation donation)
    {
        donation.CampaignId = campaignId;
        var created = _donationService.Create(donation);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // GET /v1/user/{id}/donations
    [HttpGet("user/{userId}/donations")]
    public ActionResult<List<Donation>> GetByUser(int userId)
    {
        var donations = _donationService.GetByUserId(userId);
        return Ok(donations);
    }

    // GET /v1/donations/{id}
    [HttpGet("donations/{id}")]
    public ActionResult<Donation> GetById(int id)
    {
        var donation = _donationService.GetById(id);
        if (donation == null) return NotFound();
        return Ok(donation);
    }

    // PUT /v1/donations/{id}
    [HttpPut("donations/{id}")]
    public ActionResult<Donation> Update(int id, [FromBody] Donation donation)
    {
        var updated = _donationService.Update(id, donation);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    // DELETE /v1/donations/{id}
    [HttpDelete("donations/{id}")]
    public ActionResult Delete(int id)
    {
        var deleted = _donationService.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}