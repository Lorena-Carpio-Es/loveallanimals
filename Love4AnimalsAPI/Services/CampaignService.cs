using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class CampaignService : ICampaignService
{
    private static List<Campaign> campaigns = new();
    private static int nextId = 1;

    public List<Campaign> GetAll()
    {
        return campaigns;
    }

    public Campaign GetById(int id)
    {
        return campaigns.FirstOrDefault(c => c.Id == id);
    }

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

        if (Enum.TryParse<CampaignStatus>(campaign.Status.ToString(), out var status))
            existing.Status = status;

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