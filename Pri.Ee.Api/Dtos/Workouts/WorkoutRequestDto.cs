namespace Pri.Ee.Api.Dtos.Workouts
{
    public class WorkoutRequestDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int WorkoutTypeId { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
    }
}
