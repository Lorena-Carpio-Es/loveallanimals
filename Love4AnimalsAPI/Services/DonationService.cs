using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class DonationService : IDonationService
{
    private readonly DonationRepository _repo;

    public DonationService(DonationRepository repo)
    {
        _repo = repo;
    }

    public List<Donation> GetAll() => _repo.GetAll();

    public Donation GetById(int id) => _repo.GetById(id);

    public Donation Create(Donation donation)
    {
        donation.Date = DateTime.Now;
        donation.Status = DonationStatus.Pendiente;
        return _repo.Create(donation);
    }

    public Donation Update(int id, Donation donation) => _repo.Update(id, donation);

    public bool Delete(int id) => _repo.Delete(id);

    public List<Donation> GetByCampaignId(int campaignId) => _repo.GetByCampaignId(campaignId);

    public List<Donation> GetByUserId(int userId) => _repo.GetByUserId(userId);
}