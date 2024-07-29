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
    public DbSet<IngredientsTotal> IngredientsTotal { get; set; }
    public DbSet<Instructions> Instructions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Configure Recipe to ApplicationUser relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.ApplicationUser)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Comment entity
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade delete

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade delete

        // Configure Rating entity
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade delete

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Recipe)
            .WithMany() // No collection navigation property here
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.NoAction); // No cascade delete

        // Configure IngredientsTotal and Instructions with Recipe
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.IngredientsTotal)
            .WithOne(it => it.Recipe)
            .HasForeignKey<IngredientsTotal>(it => it.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for IngredientsTotal

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Instructions)
            .WithOne(i => i.Recipe)
            .HasForeignKey<Instructions>(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Instructions

        modelBuilder.Entity<Comment>()
     .HasOne(c => c.Rating)
     .WithMany()
     .HasForeignKey(c => c.RatingId)
     .OnDelete(DeleteBehavior.SetNull); // Or DeleteBehavior.Restrict, depending on your use case

        // Configure other entities
        modelBuilder.Entity<MealPlan>()
            .HasOne(mp => mp.User)
            .WithMany(u => u.MealPlans)
            .HasForeignKey(mp => mp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IngredientsTotal>()
            .HasMany(it => it.Ingredients)
            .WithOne()
            .HasForeignKey(i => i.IngredientsTotalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Instructions>()
            .HasMany(i => i.Steps)
            .WithOne()
            .HasForeignKey(s => s.InstructionsId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Friend entity
        modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany(u => u.FriendsAdded)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friend>()
            .HasOne(f => f.FriendUser)
            .WithMany(u => u.FriendsAccepted)
            .HasForeignKey(f => f.FriendId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}


