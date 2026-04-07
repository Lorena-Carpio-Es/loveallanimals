using System.Collections.Generic;

namespace Love4AnimalsAPI.Models;

public enum CampaignStatus
{
    Active,
    Completed,
    Cancelled
}

public class Campaign
{
    public long Id { get; set; }

    public string Title { get; set; }

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

    public CampaignStatus Status { get; set; }

    public string Description { get; set; }

    
}