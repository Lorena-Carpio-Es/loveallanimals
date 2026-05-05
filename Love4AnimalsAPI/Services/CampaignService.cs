using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class CampaignService : ICampaignService
{
    private readonly AppDbContext _context;

    public CampaignService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Campaign>> GetAllAsync()
    {
        return await _context.Campaigns
            .Include(c => c.Posts)
            .ToListAsync();
    }

    public async Task<Campaign?> GetByIdAsync(int id)
    {
        return await _context.Campaigns
            .Include(c => c.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Campaign> CreateAsync(Campaign campaign)
    {
        campaign.Status = CampaignStatus.Active;

        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();

        return campaign;
    }

    public async Task<bool> UpdateAsync(int id, Campaign campaign)
    {
        var existing = await _context.Campaigns.FindAsync(id);
        if (existing == null) return false;

        existing.Title = campaign.Title;
        existing.GoalAmount = campaign.GoalAmount;
        existing.CurrentAmount = campaign.CurrentAmount;
        existing.Status = campaign.Status;
        existing.Description = campaign.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign == null) return false;

        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();
        return true;
    }
}