namespace Love4AnimalsAPI.Dto;

public class CommentResponseDto
{
    public long Id { get; set; }

    public string Text { get; set; }

    public DateTime Date { get; set; }

    public long PostId { get; set; }
}