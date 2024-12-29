using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pri.Ee.Api.Dtos.Workouts;
using Pri.Ee.Api.Dtos.WorkoutTypes;
using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Interfaces;
using Pri.EindOpdracht.Core.Services.Models;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        protected readonly IWorkoutService<Workout> _workoutService;
        private readonly IWorkoutTypeService<WorkoutType> _typeService;
        public WorkoutController(IWorkoutService<Workout> workoutService, IWorkoutTypeService<WorkoutType> typeService)
        {
            _workoutService = workoutService;
            _typeService = typeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var result = await _workoutService.ListAllAsync();

            if (result.Success)
            {
                var workoutsDto = result.Data.Select(c => new WorkoutResponseDto
                {
                    Id = c.Id,
                    CaloriesBurned = c.CaloriesBurned,
                    Date = c.Date,
                    Duration = c.Duration,
                    WorkoutTypeName = c.WorkoutType.Name,
                    UserName = $"{c.User.FirstName}_{c.User.LastName}"
                });

                return Ok(workoutsDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get(int workoutId)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _workoutService.GetByIdAsync(workoutId);

            
            if (loggedInUserId != result.Data.UserId && loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Forbid("You are not authorized to retreive workouts from another user.");
            }

            if (result.Success)
            {
                var workoutDto = new WorkoutResponseDto
                {
                    Id = result.Data.Id,
                    Date = result.Data.Date,
                    Duration = result.Data.Duration,
                    WorkoutTypeName = result.Data.WorkoutType.Name,
                    UserName = $"{result.Data.User.FirstName}_{result.Data.User.LastName}",
                    CaloriesBurned = result.Data.CaloriesBurned,
                };

                return Ok(workoutDto);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Get(string userId)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (loggedInUserId == null || loggedInUserId != userId)
            {
                return Forbid("You are not authorized to retreive workouts from another user.");
            }

            var result = await _workoutService.GetWorkoutsByUserAsync(userId);

            if (result.Success)
            {
                var workoutsDto = result.Data.Select(c => new WorkoutResponseDto
                {
                    Id = c.Id,
                    CaloriesBurned = c.CaloriesBurned,
                    Date = c.Date,
                    Duration = c.Duration,
                    WorkoutTypeName = c.WorkoutType.Name,
                    UserName = $"{c.User.FirstName}_{c.User.LastName}"
                });

                return Ok(workoutsDto);
            }

            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(WorkoutRequestDto workoutDto)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (loggedInUserId == null || loggedInUserId != workoutDto.UserId)
            {
                return Forbid("You are not authorized to add a workout for another user.");
            }

            var workout = new Workout
            {
                CaloriesBurned = workoutDto.CaloriesBurned,
                Date = workoutDto.Date,
                Duration = workoutDto.Duration,
                UserId = workoutDto.UserId,
                WorkoutTypeId = workoutDto.WorkoutTypeId,
            };

            var resultWorkout = await _workoutService.AddAsync(workout);


            if (resultWorkout.Success)
            {
                var resultType = await _typeService.GetByIdAsync(workout.WorkoutTypeId);

                if (resultType.Success)
                {
                    var dto = new WorkoutResponseDto
                    {
                        Id = workout.Id,
                        CaloriesBurned = workout.CaloriesBurned,
                        Date = workout.Date,
                        Duration = workout.Duration,
                        UserName = $"{workout.User.FirstName}_{workout.User.LastName}",
                        WorkoutTypeName = workout.WorkoutType.Name,
                        
                    };

                    return CreatedAtAction(nameof(Get), new { id = workout.Id }, dto);
                }
                return BadRequest(resultType.Errors);
            }
            return BadRequest(resultWorkout.Errors);
        }
    }
}
