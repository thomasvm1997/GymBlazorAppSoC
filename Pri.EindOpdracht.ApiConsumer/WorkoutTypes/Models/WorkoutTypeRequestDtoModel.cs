using Pri.EindOpdracht.ApiConsumer;
using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.WorkoutTypes.Models
{
    public class WorkoutTypeRequestDtoModel : BaseDtoModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "description must be between 3 and 200 characters.")]
        public string Description { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "description must be between 3 and 10 characters.")]
        public string Difficulty { get; set; }
    }
}
