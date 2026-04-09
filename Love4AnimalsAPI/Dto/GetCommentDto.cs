using System;

namespace Love4AnimalsAPI.Dto;

public class GetCommentDto
{
    public long Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

}
