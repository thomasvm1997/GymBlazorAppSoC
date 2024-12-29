using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Api.Dtos.Workouts;
using Pri.Ee.Api.Dtos.WorkoutTypes;
using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Interfaces;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IWorkoutTypeService<WorkoutType> _typeService;
        private readonly IWorkoutService<Workout> _workoutService;
        public TypeController(IWorkoutTypeService<WorkoutType> typeService, IWorkoutService<Workout> workoutService)
        {
            _typeService = typeService;
            _workoutService = workoutService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get()
        {
            var result = await _typeService.ListAllAsync();

            if (result.Success)
            {
                var typeDto = result.Data.Select(c => new WorkoutTypeResponseDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Name = c.Name,
                });

                return Ok(typeDto);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get(int typeId)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _typeService.GetByIdAsync(typeId);


            if (result.Success)
            {
                var typeDto = new WorkoutTypeRequestDto
                {
                    Id = result.Data.Id,
                    Description = result.Data.Description,
                    Difficulty = result.Data.Difficulty,
                    Name = result.Data.Name,
                };

                return Ok(typeDto);
            }

            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(WorkoutTypeRequestDto typeDto)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (loggedInUserId == null || loggedInUserId == "Customer")
            {
                return Unauthorized("You are not authorized to add a workouttype");
            }

            var type = new WorkoutType
            {
                Description = typeDto.Description,
                Difficulty = typeDto.Difficulty,
                Name = typeDto.Name,
                
            };

            var resultType = await _typeService.AddAsync(type);


            if (resultType.Success)
            {
                

                    var dto = new WorkoutTypeResponseDto
                    {
                        Id = type.Id,
                        Description = type.Description,
                        Difficulty = type.Difficulty,
                        Name = type.Name

                    };

                    return CreatedAtAction(nameof(Get), new { id = type.Id }, dto);
                
            }
            return BadRequest(resultType.Errors);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(WorkoutTypeRequestDto typeDto)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _typeService.GetByIdAsync(typeDto.Id);


            if (loggedInUserId == null || loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Unauthorized("You are not authorized to update workouttypes.");
            }



            if (result.Success == false)
            {
                return BadRequest(result.Errors);
            }

            var existingEntity = result.Data;
            existingEntity.Id = typeDto.Id;
            existingEntity.Difficulty = typeDto.Difficulty;
            existingEntity.Description = typeDto.Description;
            existingEntity.Name = typeDto.Name;

            var updateResult = await _typeService.UpdateAsync(existingEntity);

            if (updateResult.Success)
            {
                return Ok($"Product {existingEntity.Id} updated");
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _typeService.GetByIdAsync(id);
            var entity = result.Data;

            if (loggedInUserId == null || loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Unauthorized("You are not authorized to delete workouts from another user.");
            }



            if (result.Success == false)
            {
                return BadRequest(result.Errors);
            }

            var deleteResult = await _typeService.DeleteAsync(entity);

            return Ok($"Product {result.Data.Id} deleted");
        }
    }
}
