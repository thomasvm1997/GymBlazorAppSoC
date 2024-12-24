using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Entities
{
    public class WorkoutType
    {
        public int Id { get; set; }
        public string Name { get; set; } //"Cardio", "kracht"
        public string Description { get; set; } //"Workouts focusing on cardiovascular endurance"
        public string Difficulty { get; set; } // Beginner, advanced etc

        
        public ICollection<Workout> Workouts { get; set; }
    }
}
