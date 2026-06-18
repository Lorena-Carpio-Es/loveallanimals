using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface ICampaignService
{
    Task<List<Campaign>> GetAllAsync();
    Task<Campaign?> GetByIdAsync(long id);
    Task<Campaign> CreateAsync(Campaign campaign);
    Task<bool> UpdateAsync(long id, Campaign campaign);
    Task<bool> DeleteAsync(long id);
}