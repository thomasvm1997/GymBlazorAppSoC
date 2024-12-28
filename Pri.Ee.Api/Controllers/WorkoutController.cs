using Microsoft.AspNetCore.Mvc;
using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Interfaces;

namespace Pri.Ee.Api.Controllers
{
    public class WorkoutController : ControllerBase
    {
        protected readonly IWorkoutService<Workout> _workoutService;
        private readonly IWorkoutTypeService<WorkoutType> _typeService;
        public WorkoutController(IWorkoutService<Workout> workoutService, IWorkoutTypeService<WorkoutType> typeService)
        {
            _workoutService = workoutService;
            _typeService = typeService;
        }
    }
}
