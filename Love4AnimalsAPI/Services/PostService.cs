using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _context;
    private readonly ICacheService _cache;

    public PostService(AppDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        const string cacheKey = "posts:all";

        var cachedPosts = await _cache.GetAsync<List<Post>>(cacheKey);

        if (cachedPosts != null)
            return cachedPosts;

        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Campaign)
            .Include(p => p.Comments)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, posts, 5);

        return posts;
    }

    public async Task<Post?> GetByIdAsync(long id)
    {
        var cacheKey = $"posts:{id}";

        var cachedPost = await _cache.GetAsync<Post>(cacheKey);

        if (cachedPost != null)
            return cachedPost;

        var post = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Campaign)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post != null)
            await _cache.SetAsync(cacheKey, post, 5);

        return post;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        var user = await _context.Users.FindAsync(post.UserId);

        if (user == null)
            throw new Exception("El usuario no existe");

        var campaign = await _context.Campaigns.FindAsync(post.CampaignId);

        if (campaign == null)
            throw new Exception("La campaña no existe");

        post.CreationDate = DateTime.UtcNow;
        post.State = campaign.Status;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("posts:all");
        await _cache.RemoveAsync("campaigns:all");

        return post;
    }

    public async Task<bool> UpdateAsync(long id, Post post)
    {
        var existing = await _context.Posts.FindAsync(id);

        if (existing == null)
            return false;

        existing.Title = post.Title;
        existing.Description = post.Description;
        existing.FundraisingGoal = post.FundraisingGoal;
        existing.Image = post.Image;

        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("posts:all");
        await _cache.RemoveAsync($"posts:{id}");

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("posts:all");
        await _cache.RemoveAsync($"posts:{id}");
        await _cache.RemoveAsync("campaigns:all");

        return true;
    }

    public async Task LikeAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post != null)
        {
            post.QuantityLikes++;
            await _context.SaveChangesAsync();

            await _cache.RemoveAsync("posts:all");
            await _cache.RemoveAsync($"posts:{id}");
        }
    }

    public async Task ShareAsync(long id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post != null)
        {
            post.QuantityShared++;
            await _context.SaveChangesAsync();

            await _cache.RemoveAsync("posts:all");
            await _cache.RemoveAsync($"posts:{id}");
        }
    }
}