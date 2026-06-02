using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class CampaignService : ICampaignService
{
    private readonly AppDbContext _context;
    private readonly ICacheService _cache;

    public CampaignService(AppDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<Campaign>> GetAllAsync()
    {
        const string cacheKey = "campaigns:all";

        var cachedCampaigns = await _cache.GetAsync<List<Campaign>>(cacheKey);

        if (cachedCampaigns != null)
            return cachedCampaigns;

        var campaigns = await _context.Campaigns
            .AsNoTracking()
            .Include(c => c.Posts)
            .Include(c => c.Donations)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, campaigns, 5);

        return campaigns;
    }

    public async Task<Campaign?> GetByIdAsync(long id)
    {
        var cacheKey = $"campaigns:{id}";

        var cachedCampaign = await _cache.GetAsync<Campaign>(cacheKey);

        if (cachedCampaign != null)
            return cachedCampaign;

        var campaign = await _context.Campaigns
            .AsNoTracking()
            .Include(c => c.Posts)
            .Include(c => c.Donations)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campaign != null)
            await _cache.SetAsync(cacheKey, campaign, 5);

        return campaign;
    }

    public async Task<Campaign> CreateAsync(Campaign campaign)
    {
        campaign.Status = CampaignStatus.Active;

        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("campaigns:all");

        return campaign;
    }

    public async Task<bool> UpdateAsync(long id, Campaign campaign)
    {
        var existing = await _context.Campaigns.FindAsync(id);

        if (existing == null)
            return false;

        existing.Title = campaign.Title;
        existing.GoalAmount = campaign.GoalAmount;
        existing.CurrentAmount = campaign.CurrentAmount;
        existing.Status = campaign.Status;
        existing.Description = campaign.Description;

        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("campaigns:all");
        await _cache.RemoveAsync($"campaigns:{id}");

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);

        if (campaign == null)
            return false;

        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("campaigns:all");
        await _cache.RemoveAsync($"campaigns:{id}");

        return true;
    }
}