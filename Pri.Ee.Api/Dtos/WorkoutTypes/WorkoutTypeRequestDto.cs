using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Api.Dtos.WorkoutTypes
{
    public class WorkoutTypeRequestDto : BaseDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "description must be between 3 and 50 characters.")]
        public string Description { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Diffuclty must be between 3 and 50 characters.")]
        public string Difficulty { get; set; }
    }
}
