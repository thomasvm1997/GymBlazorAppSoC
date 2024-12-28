using Pri.EindOpdracht.Core.Entities;

namespace Pri.Ee.Api.Dtos.Workouts
{
    public class WorkoutResponseDto : BaseDto
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string WorkoutTypeName { get; set; } // Name of the WorkoutType
        public string? UserName { get; set; } //Users full name firstname + lastname
    }
}
