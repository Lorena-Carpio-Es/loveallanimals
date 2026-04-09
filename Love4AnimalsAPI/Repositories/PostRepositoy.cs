using System;
using System.Collections.Generic;
using System.Linq;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Repositories;

public class PostRepository
{
    private static readonly List<Post> posts = new();
    private static long nextId = 1;

    public List<Post> GetAll() => posts;

    public Post GetById(long id) => posts.FirstOrDefault(p => p.Id == id);

    public List<Post> GetByUser(long userId) => posts.Where(p => p.UserId == userId).ToList();

    public Post Create(Post pub)
    {
        pub.Id = nextId++;
        pub.CreationDate = DateTime.Now;
        pub.State ??= "Active";
        posts.Add(pub);
        return pub;
    }

    public Post Update(long id, Post pub)
    {
        var existing = posts.FirstOrDefault(p => p.Id == id);
        if (existing == null) return null;

        existing.Title = pub.Title;
        existing.Description = pub.Description;
        existing.FundraisingGoal = pub.FundraisingGoal;
        existing.Image = pub.Image;
        existing.State = pub.State;
        return existing;
    }

    public bool Delete(long id)
    {
        var existing = posts.FirstOrDefault(p => p.Id == id);
        if (existing == null) return false;
        posts.Remove(existing);
        return true;
    }

    public void DarLike(long id)
    {
        var p = posts.FirstOrDefault(x => x.Id == id);
        if (p != null) p.QuantityLikes++;
    }

    public void Compartir(long id)
    {
        var p = posts.FirstOrDefault(x => x.Id == id);
        if (p != null) p.QuantityShared++;
    }
}