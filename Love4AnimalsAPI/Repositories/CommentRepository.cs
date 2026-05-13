using System;
using System.Collections.Generic;
using System.Linq;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Repositories;

public class CommentRepository
{
    private static readonly List<Comment> comments = new();
    private static long nextId = 1;

    public List<Comment> GetByPost(long postId) => comments.Where(c => c.PostId == postId).ToList();

    public Comment Create(long postId, Comment comment)
    {
        comment.Id = nextId++;
        comment.PostId = postId;
        comment.Date = DateTime.Now;
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
        var existing = comments.FirstOrDefault(c => c.Id == id);
        if (existing == null) return false;
        comments.Remove(existing);
        return true;
    }
}