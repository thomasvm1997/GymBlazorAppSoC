namespace Pri.Ee.Api.Dtos.Goals
{
    public class GoalRequestDto
    {
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; }
    }
}
