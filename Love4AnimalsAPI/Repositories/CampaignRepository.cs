using System;
using System.Collections.Generic;
using System.Linq;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Repositories;

public class CampaignRepository
{
    private static readonly List<Campaign> campaigns = new();
    private static int nextId = 1;

    public List<Campaign> GetAll() => campaigns;

    public Campaign GetById(int id) => campaigns.FirstOrDefault(c => c.Id == id);

    public Campaign Create(Campaign campaign)
    {
        campaign.Id = nextId++;
        campaign.CurrentAmount = 0;
        campaign.Status = CampaignStatus.Active;
        campaigns.Add(campaign);
        return campaign;
    }

    public Campaign Update(int id, Campaign campaign)
    {
        var existing = campaigns.FirstOrDefault(c => c.Id == id);
        if (existing == null) return null;

        existing.Title = campaign.Title;
        existing.GoalAmount = campaign.GoalAmount;
        existing.CurrentAmount = campaign.CurrentAmount;
        existing.Description = campaign.Description;

        // Mantener el enum correctamente
        existing.Status = campaign.Status;

        return existing;
    }

    public bool Delete(int id)
    {
        var campaign = campaigns.FirstOrDefault(c => c.Id == id);
        if (campaign == null) return false;
        campaigns.Remove(campaign);
        return true;
    }
}