using System;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Services;

public class CommentService : ICommentService
{
    private static List<Comment> comments = new();
    private static long idCounter = 1;

    public List<Comment> GetByPost(long postId)
        => comments.Where(c => c.PostId == postId).ToList();

    public Comment Create(long postId, Comment comment)
    {
        comment.Id = idCounter++;
        comment.Date = DateTime.Now;
        comment.PostId = postId;

        comments.Add(comment);
        return comment;
    }

    public Comment Update(long id, Comment comment)
    {
        var existing = comments.FirstOrDefault(c => c.Id == id);
        if (existing == null) return null;

        existing.Text = comment.Text;
        return existing;
    }

    public bool Delete(long id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return false;

        comments.Remove(comment);
        return true;
    }

}
