using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class CampaignService : ICampaignService
{
    private readonly CampaignRepository _repo;

    public CampaignService(CampaignRepository repo)
    {
        _repo = repo;
    }

    public List<Campaign> GetAll() => _repo.GetAll();

    public Campaign GetById(int id) => _repo.GetById(id);

    public Campaign Create(Campaign campaign) => _repo.Create(campaign);

    public Campaign Update(int id, Campaign campaign) => _repo.Update(id, campaign);

    public bool Delete(int id) => _repo.Delete(id);
}