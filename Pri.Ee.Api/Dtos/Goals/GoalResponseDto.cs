namespace Pri.Ee.Api.Dtos.Goals
{
    public class GoalResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
