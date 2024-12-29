using Pri.EindOpdracht.ApiConsumer;
using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Workouts.Models
{
    public class WorkoutRequestDtoModel : BaseDtoModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int WorkoutTypeId { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int CaloriesBurned { get; set; }
    }
}
