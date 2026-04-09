using System;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class CommentService : ICommentService
{
    private readonly CommentRepository _repo;

    public CommentService(CommentRepository repo)
    {
        _repo = repo;
    }

    public List<Comment> GetByPost(long postId) => _repo.GetByPost(postId);

    public Comment Create(long postId, Comment comment) => _repo.Create(postId, comment);

    public Comment Update(long id, Comment comment) => _repo.Update(id, comment);

    public bool Delete(long id) => _repo.Delete(id);
}