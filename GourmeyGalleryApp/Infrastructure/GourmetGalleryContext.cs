using GourmeyGalleryApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GourmetGallery.Infrastructure;

public class GourmetGalleryContext : IdentityDbContext<ApplicationUser>
{
    public GourmetGalleryContext(DbContextOptions<GourmetGalleryContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Recipe to ApplicationUser relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.ApplicationUser)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction instead of Cascade

        // Configure Friend entity
        modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany(u => u.FriendsAdded)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        modelBuilder.Entity<Friend>()
            .HasOne(f => f.FriendUser)
            .WithMany(u => u.FriendsAccepted)
            .HasForeignKey(f => f.FriendId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        // Configure Comment entity
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // This can remain Cascade

        // Configure Rating entity
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Recipe)
            .WithMany(r => r.Ratings)
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // This can remain Cascade

        // Configure MealPlan entity
        modelBuilder.Entity<MealPlan>()
            .HasOne(mp => mp.User)
            .WithMany(u => u.MealPlans)
            .HasForeignKey(mp => mp.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        // Configure Message entity
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction
    }



}


