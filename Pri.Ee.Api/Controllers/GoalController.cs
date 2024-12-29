using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Api.Dtos.Goals;
using Pri.Ee.Api.Dtos.Workouts;
using Pri.EindOpdracht.Core;
using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Interfaces;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalService<Goal> _goalService;
        public GoalController(IGoalService<Goal> goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var result = await _goalService.ListAllAsync();

            if (result.Success)
            {
                var goalDto = result.Data.Select(c => new GoalResponseDto
                {
                    Id = c.Id,
                    Achieved = c.Achieved,
                    Description = c.Description,
                    TargetDate = c.TargetDate,
                    UserName = $"{c.User.FirstName}_{c.User.LastName}"
                });

                return Ok(goalDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Get(int goalId)
        {
            var loggedInUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var loggedInUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var result = await _goalService.GetByIdAsync(goalId);


            if (loggedInUserId != result.Data.UserId && loggedInUserRole == "Customer") //checksje uitvoeren om er voor te zorgen dat je egen workouts kan bekijken
            {                                                                           //van andere customers
                return Unauthorized("You are not authorized to retreive goals from another user.");
            }

            if (result.Success)
            {
                var goalDto = new GoalResponseDto
                {
                    Id = result.Data.Id,
                    Achieved = result.Data.Achieved,
                    Description = result.Data.Description,
                    TargetDate  = result.Data.TargetDate,
                    UserName = $"{result.Data.User.FirstName}_{result.Data.User.LastName}",

                };

                return Ok(goalDto);
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
                return Unauthorized("You are not authorized to retreive goals from another user.");
            }

            var result = await _goalService.GetGoalsByUserAsync(userId);

            if (result.Success)
            {
                var goalDto = result.Data.Select(c => new GoalResponseDto
                {
                    Id = c.Id,
                    Achieved = c.Achieved,
                    Description = c.Description ,
                    TargetDate = c.TargetDate,
                    UserName = $"{c.User.FirstName}_{c.User.LastName}",
                });

                return Ok(goalDto);
            }

            return BadRequest(result.Errors);
        }
    }
}
