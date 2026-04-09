using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IDonationService
{
    List<Donation> GetAll();

    Donation GetById(int id);

    Donation Create(Donation donation);

    Donation Update(int id, Donation donation);

    bool Delete(int id);
    
    // Métodos adicionales necesarios según los endpoints
    List<Donation> GetByCampaignId(int campaignId);
    
    List<Donation> GetByUserId(int userId);
}