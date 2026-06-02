using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class DonationService : IDonationService
{
    private readonly AppDbContext _context;

    public DonationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Donation>> GetAllAsync()
    {
        return await _context.Donations
            .Include(d => d.User)
            .Include(d => d.Campaign)
            .ToListAsync();
    }

    public async Task<Donation?> GetByIdAsync(long id)
    {
        return await _context.Donations
            .Include(d => d.User)
            .Include(d => d.Campaign)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Donation>> GetByCampaignAsync(long campaignId)
    {
        return await _context.Donations
            .Include(d => d.User)
            .Where(d => d.CampaignId == campaignId)
            .ToListAsync();
    }

    public async Task<List<Donation>> GetByUserAsync(long userId)
    {
        return await _context.Donations
            .Include(d => d.Campaign)
            .Where(d => d.UserId == userId)
            .ToListAsync();
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

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var donation = await _context.Donations.FindAsync(id);

        if (donation == null)
            return false;

        _context.Donations.Remove(donation);

        await _context.SaveChangesAsync();

        return true;
    }
}