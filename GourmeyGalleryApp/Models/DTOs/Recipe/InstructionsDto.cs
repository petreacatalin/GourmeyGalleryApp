namespace GourmeyGalleryApp.Models.DTOs.Recipe
{
    public class InstructionsDto
    {
        public int Id { get; set; }
        public List<StepDto> Steps { get; set; }
    }
}
