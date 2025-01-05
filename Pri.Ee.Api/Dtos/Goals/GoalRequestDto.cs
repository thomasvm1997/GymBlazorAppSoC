using Pri.Ee.Api.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Api.Dtos.Goals
{
    public class GoalRequestDto : BaseDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "description must be between 3 and 100 characters.")]
        public string Description { get; set; }
        [Required]
        [FutureDate]
        public DateTime TargetDate { get; set; }
        
        public bool Achieved { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
