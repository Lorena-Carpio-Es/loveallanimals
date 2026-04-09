using System;

namespace Love4AnimalsAPI.Dto;

public class GetDonationDto
{
    public long Id { get; set; }
        public long UserId { get; set; }
        public string UserAlias { get; set; }
        public long CampaignId { get; set; }
        public string CampaignTitle { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

}
