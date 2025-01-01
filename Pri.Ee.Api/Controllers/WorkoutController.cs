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
                    UserName = $"{c.User.FirstName}_{c.User.LastName}",
                    UserId = c.UserId,
                    
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
                return Unauthorized("You are not authorized to retreive workouts from another user.");
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
                    UserId = result.Data.UserId
                };

                return Ok(workoutDto);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Get(string userId)
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"Authorization Header: {authHeader}");
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (loggedInUserId == null || loggedInUserId != userId)
            {
                return Unauthorized("You are not authorized to retreive workouts from another user.");
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
                    UserName = $"{c.User.FirstName}_{c.User.LastName}",
                    UserId = c.UserId
                    
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
                return Unauthorized("You are not authorized to add a workout for another user.");
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
                        UserId = workout.UserId
                    };

                    return CreatedAtAction(nameof(Get), new { id = workout.Id }, dto);
                }
                return BadRequest(resultType.Errors);
            }
            return BadRequest(resultWorkout.Errors);
        }
        [HttpPut]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> Update(WorkoutRequestDto workoutDto)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _workoutService.GetByIdAsync(workoutDto.Id);


            if (loggedInUserId != result.Data.UserId && loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Unauthorized("You are not authorized to update workouts from another user.");
            }

            

            if (result.Success == false)
            {
                return BadRequest(result.Errors);
            }

            var existingEntity = result.Data;
            existingEntity.Duration = workoutDto.Duration;
            existingEntity.CaloriesBurned = workoutDto.CaloriesBurned;
            existingEntity.Id = workoutDto.Id;
            existingEntity.WorkoutTypeId = workoutDto.WorkoutTypeId;
            existingEntity.UserId = workoutDto.UserId;

            var updateResult = await _workoutService.UpdateAsync(existingEntity);

            if (updateResult.Success)
            {
                return Ok($"Product {existingEntity.Id} updated");
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _workoutService.GetByIdAsync(id);
            var entity = result.Data;

            if (loggedInUserId != result.Data.UserId && loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Unauthorized("You are not authorized to delete workouts from another user.");
            }

            

            if (result.Success == false)
            {
                return BadRequest(result.Errors);
            }

            var deleteResult = await _workoutService.DeleteAsync(entity);

            return Ok($"Product {result.Data.Id} deleted");
        }
    }
}
