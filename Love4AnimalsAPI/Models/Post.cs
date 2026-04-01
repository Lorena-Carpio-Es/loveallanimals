using System;

namespace Love4AnimalsAPI.Models;

public class Post
{
     public long Id { get; set; }
        public string Title { get; set; }
        public double FundraisingGoal { get; set; }
        public double CurrentAmount { get; set; }
        public string State { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public int QuantityLikes { get; set; } = 0;
        public int QuantityComments { get; set; } = 0;
        public int QuantityShared { get; set; } = 0;

        public long UserId { get; set; }
        public long CampaignId { get; set; }

}
