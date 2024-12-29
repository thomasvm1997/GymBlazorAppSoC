using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Goals.Models
{
    public class GoalRequestDtoModel : BaseDtoModel
    {
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; }
        public string UserId { get; set; }
    }
}
