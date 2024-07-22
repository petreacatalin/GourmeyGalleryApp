namespace GourmeyGalleryApp.Models.Entities
{
    public class Friend
    {
        public int Id { get; set; }

        // Foreign key to the user who initiated the friend request
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key to the user who accepted the friend request
        public string FriendId { get; set; }
        public ApplicationUser FriendUser { get; set; }
    }
}
