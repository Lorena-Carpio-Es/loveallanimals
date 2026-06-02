using System;
using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _context;

    public PostService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Campaign)
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(long id)
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> CreateAsync(Post post)
    {
       post.CreationDate = DateTime.UtcNow;
        post.State = "Active";

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return post;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task LikeAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            post.QuantityLikes++;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ShareAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            post.QuantityShared++;
            await _context.SaveChangesAsync();
        }
    }
}