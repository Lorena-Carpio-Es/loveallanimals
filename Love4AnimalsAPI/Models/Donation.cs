using System;

namespace Love4AnimalsAPI.Models;

public class Donation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CampaignId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public DonationStatus Status { get; set; }
}

public enum DonationStatus
{
    Pendiente,
    Confirmada,
    Cancelada
}