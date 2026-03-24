using System;

using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Interfaces;

public interface ICampaignService
{
    List<Campaign> GetAll();

    Campaign GetById(int id);

    Campaign Create(Campaign campaign);

    Campaign Update(int id, Campaign campaign);

    bool Delete(int id);

}
