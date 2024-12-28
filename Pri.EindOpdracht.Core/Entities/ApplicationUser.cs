using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        // Navigation properties for related entities
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }

}
