namespace Love4AnimalsAPI.Models;

public enum DonationStatus
{
    Pending,
    Confirmed,
    Cancelled
}

public class Donation
{
    public long Id { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }

    public DonationStatus Status { get; set; }

    
    public long UserId { get; set; }
    public User User { get; set; }

      public long CampaignId { get; set; }
    public Campaign Campaign { get; set; }
}