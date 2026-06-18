namespace Love4AnimalsAPI.Dto;

public class PostResponseDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public double FundraisingGoal { get; set; }

    public string Image { get; set; }

    public DateTime CreationDate { get; set; }

    public string State { get; set; }

    public int QuantityLikes { get; set; }

    public int QuantityShared { get; set; }

    public int QuantityComments { get; set; }

    public long UserId { get; set; }

    public string UserName { get; set; }

    public long CampaignId { get; set; }

    public string CampaignTitle { get; set; }
}