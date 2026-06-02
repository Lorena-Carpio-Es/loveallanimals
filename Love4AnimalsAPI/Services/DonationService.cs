using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class DonationService : IDonationService
{
    private readonly AppDbContext _context;
    private readonly ICacheService _cache;

    public DonationService(AppDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<Donation>> GetAllAsync()
    {
        const string cacheKey = "donations:all";

        var cachedDonations = await _cache.GetAsync<List<Donation>>(cacheKey);

        if (cachedDonations != null)
            return cachedDonations;

        var donations = await _context.Donations
            .AsNoTracking()
            .Include(d => d.User)
            .Include(d => d.Campaign)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, donations, 5);

        return donations;
    }

    public async Task<Donation?> GetByIdAsync(long id)
    {
        var cacheKey = $"donations:{id}";

        var cachedDonation = await _cache.GetAsync<Donation>(cacheKey);

        if (cachedDonation != null)
            return cachedDonation;

        var donation = await _context.Donations
            .AsNoTracking()
            .Include(d => d.User)
            .Include(d => d.Campaign)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (donation != null)
            await _cache.SetAsync(cacheKey, donation, 5);

        return donation;
    }

    public async Task<List<Donation>> GetByCampaignAsync(long campaignId)
    {
        var cacheKey = $"donations:campaign:{campaignId}";

        var cachedDonations = await _cache.GetAsync<List<Donation>>(cacheKey);

        if (cachedDonations != null)
            return cachedDonations;

        var donations = await _context.Donations
            .AsNoTracking()
            .Include(d => d.User)
            .Where(d => d.CampaignId == campaignId)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, donations, 5);

        return donations;
    }

    public async Task<List<Donation>> GetByUserAsync(long userId)
    {
        var cacheKey = $"donations:user:{userId}";

        var cachedDonations = await _cache.GetAsync<List<Donation>>(cacheKey);

        if (cachedDonations != null)
            return cachedDonations;

        var donations = await _context.Donations
            .AsNoTracking()
            .Include(d => d.Campaign)
            .Where(d => d.UserId == userId)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, donations, 5);

        return donations;
    }

    public async Task<Donation> CreateAsync(Donation donation)
    {
        var user = await _context.Users.FindAsync(donation.UserId);

        if (user == null)
            throw new Exception("El usuario no existe");

        var campaign = await _context.Campaigns.FindAsync(donation.CampaignId);

        if (campaign == null)
            throw new Exception("La campaña no existe");

        donation.Date = DateTime.UtcNow;
        donation.Status = DonationStatus.Pending;

        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("donations:all");
        await _cache.RemoveAsync($"donations:campaign:{donation.CampaignId}");
        await _cache.RemoveAsync($"donations:user:{donation.UserId}");
        await _cache.RemoveAsync("campaigns:all");
        await _cache.RemoveAsync($"campaigns:{donation.CampaignId}");

        return donation;
    }

    public async Task<bool> UpdateAsync(long id, Donation donation)
    {
        var existing = await _context.Donations.FindAsync(id);

        if (existing == null)
            return false;

        existing.Amount = donation.Amount;
        existing.Status = donation.Status;

        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("donations:all");
        await _cache.RemoveAsync($"donations:{id}");
        await _cache.RemoveAsync($"donations:campaign:{existing.CampaignId}");
        await _cache.RemoveAsync($"donations:user:{existing.UserId}");

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var donation = await _context.Donations.FindAsync(id);

        if (donation == null)
            return false;

        var campaignId = donation.CampaignId;
        var userId = donation.UserId;

        _context.Donations.Remove(donation);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("donations:all");
        await _cache.RemoveAsync($"donations:{id}");
        await _cache.RemoveAsync($"donations:campaign:{campaignId}");
        await _cache.RemoveAsync($"donations:user:{userId}");
        await _cache.RemoveAsync("campaigns:all");
        await _cache.RemoveAsync($"campaigns:{campaignId}");

        return true;
    }
}