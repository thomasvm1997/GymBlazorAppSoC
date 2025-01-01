namespace Pri.EindOpdracht.ApiConsumer.Goals.Models
{
    public class GoalResponseDtoModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
