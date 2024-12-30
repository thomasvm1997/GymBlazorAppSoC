using Pri.EindOpdracht.ApiConsumer;
using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Workouts.Models
{
    public class WorkoutRequestDtoModel : BaseDtoModel
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int WorkoutTypeId { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
    }
}
