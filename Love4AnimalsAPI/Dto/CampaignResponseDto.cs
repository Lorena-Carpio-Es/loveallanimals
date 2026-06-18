namespace Love4AnimalsAPI.Dto;

public class CampaignResponseDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

    public string Status { get; set; }

    public string Description { get; set; }

    public int TotalPosts { get; set; }

    public int TotalDonations { get; set; }
}