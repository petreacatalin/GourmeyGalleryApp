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
    public DbSet<NutritionFacts> NutritionFacts { get; set; }
    public DbSet<InformationTime> InformationTimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Recipe to ApplicationUser relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.ApplicationUser)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Comment entity
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Recipe)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Rating)
            .WithMany()
            .HasForeignKey(c => c.RatingId)
            .OnDelete(DeleteBehavior.SetNull); // Ensure no cascade delete here

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade delete

        // Rating entity
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Recipe)
            .WithMany()
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Recipe to IngredientsTotal and Instructions
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.IngredientsTotal)
            .WithOne(it => it.Recipe)
            .HasForeignKey<IngredientsTotal>(it => it.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Instructions)
            .WithOne(i => i.Recipe)
            .HasForeignKey<Instructions>(i => i.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Other entities
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

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.NutritionFacts)
            .WithOne(nf => nf.Recipe)
            .HasForeignKey<NutritionFacts>(nf => nf.RecipeId);

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.InformationTime)
            .WithOne(it => it.Recipe)
            .HasForeignKey<InformationTime>(it => it.RecipeId);        
    }

}


