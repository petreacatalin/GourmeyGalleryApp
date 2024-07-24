namespace GourmeyGalleryApp.Models.DTOs
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
