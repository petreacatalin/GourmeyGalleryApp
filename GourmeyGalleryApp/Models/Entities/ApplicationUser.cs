using Microsoft.AspNetCore.Identity;

namespace GourmeyGalleryApp.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        // Navigation property representing friends added by the user
        public ICollection<Friend> FriendsAdded { get; set; }

        // Navigation property representing friends who added the user
        public ICollection<Friend> FriendsAccepted { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<MealPlan> MealPlans { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
