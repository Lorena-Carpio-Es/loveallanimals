using System;
using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Interfaces;

public interface ICommentService
{
    List<Comment> GetByPost(long postId);
    Comment Create(long postId, Comment comment);
    Comment Update(long id, Comment comment);
    bool Delete(long id);

}
