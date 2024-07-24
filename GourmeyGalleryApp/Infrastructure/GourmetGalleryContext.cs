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
    public DbSet<Review> Reviews { get; set; }
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
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction

        //modelBuilder.Entity<Review>()
        //    .HasOne(r => r.Recipe)
        //    .WithMany(r => r.Reviews)
        //    .HasForeignKey(r => r.RecipeId)
        //    .OnDelete(DeleteBehavior.Cascade); // This can remain Cascade

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
                                                // Configure Recipe entity
                                                // Configure Recipe to Ingredient relationship
                                                // Configure Recipe to IngredientsTotal relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.IngredientsTotal)
            .WithOne(it => it.Recipe)
            .HasForeignKey<IngredientsTotal>(it => it.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

        // Configure Recipe to Instructions relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Instructions)
            .WithOne(i => i.Recipe)
            .HasForeignKey<Instructions>(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

        // Configure IngredientsTotal to Ingredient relationship
        modelBuilder.Entity<IngredientsTotal>()
            .HasMany(it => it.Ingredients)
            .WithOne() // Ingredients don't need navigation property back to IngredientsTotal
            .HasForeignKey(i => i.IngredientsTotalId)
            .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

        // Configure Instructions to Step relationship
        modelBuilder.Entity<Instructions>()
            .HasMany(i => i.Steps)
            .WithOne() // Steps don't need navigation property back to Instructions
            .HasForeignKey(s => s.InstructionsId)
            .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior


    }

}


