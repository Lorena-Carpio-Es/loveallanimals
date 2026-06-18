using System;
using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Interfaces;

public interface ICommentService
{
    Task<List<Comment>> GetByPostAsync(long postId);
    Task<Comment> CreateAsync(Comment comment);
    Task<bool> DeleteAsync(long id);
    Task<bool> UpdateAsync(long id, Comment comment); 
}
