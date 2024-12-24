namespace Pri.EindOpdracht.Core.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkoutTypeId { get; set; } 
        public WorkoutType WorkoutType { get; set; } // Navigatie
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; } //Vagigatie property om gemakkelijk queries uit te voeren
    }
}
