namespace GourmeyGalleryApp.Models.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public int InstructionsId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
    }
}
