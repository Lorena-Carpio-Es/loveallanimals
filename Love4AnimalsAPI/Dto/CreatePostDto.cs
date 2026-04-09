using System;
using Microsoft.AspNetCore.Http;
namespace Love4AnimalsAPI.Dto;

public class CreatePostDto
{
    public string Title { get; set; }
    public double FundraisingGoal { get; set; }
    public string Description { get; set; }
    public string    Image { get; set; }
    public long UserId { get; set; }
    public long CampaignId { get; set; }

}
