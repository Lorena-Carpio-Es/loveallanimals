namespace Love4AnimalsAPI.Dto;

public class GetCampaignDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }
}