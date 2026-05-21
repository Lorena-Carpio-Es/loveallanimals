using System;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetByPostAsync(long postId)
    {
        return await _context.Comments
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        comment.Date = DateTime.Now;
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> UpdateAsync(long id, Comment comment)
{
    var existing = await _context.Comments.FindAsync(id);
    if (existing == null) return false;

    existing.Text = comment.Text;

    await _context.SaveChangesAsync();
    return true;
}

    public async Task<bool> DeleteAsync(long id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null) return false;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }
}