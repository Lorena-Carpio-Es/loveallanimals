using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetAllAsync();

    Task<Post?> GetByIdAsync(long id);

    Task<Post> CreateAsync(Post post);

    Task<bool> UpdateAsync(long id, Post post);

    Task<bool> DeleteAsync(long id);

    Task LikeAsync(long id);

    Task ShareAsync(long id);
}