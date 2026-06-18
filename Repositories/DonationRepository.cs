using System.Collections.Generic;
using System.Linq;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Repositories;

public class DonationRepository
{
    private static readonly List<Donation> donations = new();
    private static int nextId = 1;

    public List<Donation> GetAll() => donations;

    public Donation GetById(int id) => donations.FirstOrDefault(d => d.Id == id);

    public Donation Create(Donation donation)
    {
        donation.Id = nextId++;
        donations.Add(donation);
        return donation;
    }

    public Donation Update(int id, Donation donation)
    {
        var existing = donations.FirstOrDefault(d => d.Id == id);
        if (existing == null) return null;

        existing.Amount = donation.Amount;
        existing.UserId = donation.UserId;
        existing.CampaignId = donation.CampaignId;
        existing.Status = donation.Status;

        return existing;
    }

    public bool Delete(int id)
    {
        var existing = donations.FirstOrDefault(d => d.Id == id);
        if (existing == null) return false;
        donations.Remove(existing);
        return true;
    }

    public List<Donation> GetByCampaignId(int campaignId) =>
        donations.Where(d => d.CampaignId == campaignId).ToList();

    public List<Donation> GetByUserId(int userId) =>
        donations.Where(d => d.UserId == userId).ToList();
}