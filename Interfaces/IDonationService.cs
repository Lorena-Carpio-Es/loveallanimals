using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IDonationService
{
    Task<List<Donation>> GetAllAsync();

    Task<Donation?> GetByIdAsync(long id);

    Task<List<Donation>> GetByCampaignAsync(long campaignId);

    Task<List<Donation>> GetByUserAsync(long userId);

    Task<Donation> CreateAsync(Donation donation);

    Task<bool> UpdateAsync(long id, Donation donation);

    Task<bool> DeleteAsync(long id);
}