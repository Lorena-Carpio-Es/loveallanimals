namespace Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Models;

public class UpdateCampaignDto
{
    public string Title { get; set; }

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

     public CampaignStatus Status { get; set; }

    public string Description { get; set; }
}