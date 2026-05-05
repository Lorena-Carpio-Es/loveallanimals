using System;

using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Interfaces;

public interface ICampaignService
{
    Task<List<Campaign>> GetAllAsync();
    Task<Campaign?> GetByIdAsync(int id);
    Task<Campaign> CreateAsync(Campaign campaign);
    Task<bool> UpdateAsync(int id, Campaign campaign);
    Task<bool> DeleteAsync(int id);
}
