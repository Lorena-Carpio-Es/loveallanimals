using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Dto;

public class UpdateDonationDto
{
    public double Amount { get; set; }

    public DonationStatus Status { get; set; }
}