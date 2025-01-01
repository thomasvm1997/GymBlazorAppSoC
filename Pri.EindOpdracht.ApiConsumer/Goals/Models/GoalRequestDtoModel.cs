using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Goals.Models
{
    public class GoalRequestDtoModel : BaseDtoModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "description must be between 3 and 200 characters.")]
        public string Description { get; set; }
        [Required]
        public DateTime TargetDate { get; set; }

        public bool Achieved { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
