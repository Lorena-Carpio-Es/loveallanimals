using System;
using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Post> Posts { get; set; }
     public DbSet<Comment> Comments { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
    // Post → Campaign
    modelBuilder.Entity<Post>()
        .HasOne(p => p.Campaign)
        .WithMany(c => c.Posts)
        .HasForeignKey(p => p.CampaignId);

    // Post → User
    modelBuilder.Entity<Post>()
        .HasOne(p => p.User)
        .WithMany(u => u.Posts)
        .HasForeignKey(p => p.UserId);
   }   

}

