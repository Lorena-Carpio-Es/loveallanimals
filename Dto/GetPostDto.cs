using System;

namespace Love4AnimalsAPI.Dto;

public class GetPostDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public double CurrentAmount { get; set; }
    public int QuantityLikes { get; set; }

}
