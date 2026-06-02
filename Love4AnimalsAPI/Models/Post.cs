using System;

namespace Love4AnimalsAPI.Models;

public class Post
{
    public long Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public double FundraisingGoal { get; set; }
    public string Image { get; set; }

    public DateTime CreationDate { get; set; }
    public CampaignStatus  State { get; set; }

    public int QuantityLikes { get; set; }
    public int QuantityShared { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long CampaignId { get; set; }
    public Campaign Campaign { get; set; }

    public List<Comment> Comments { get; set; }
}