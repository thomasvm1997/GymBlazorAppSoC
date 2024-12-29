using Pri.EindOpdracht.ApiConsumer;

namespace Pri.EindOpdracht.ApiConsumer.WorkoutTypes.Models
{
    public class WorkoutTypeRequestDtoModel : BaseDtoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
    }
}
