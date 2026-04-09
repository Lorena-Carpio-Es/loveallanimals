using System;

namespace Love4AnimalsAPI.Models;

public class Comment
{
     public long Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public long PostId { get; set; } // 🔥 relación
}


