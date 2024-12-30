using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Goals
{
    public interface IGoalApiService
    {
        Task<GoalResponseDtoModel[]> GetGoalsAsync(string token);
        Task<GoalResponseDtoModel> GetGoalByIdAsync(int id, string token);
        Task<GoalResponseDtoModel[]> GetGoalsByUserIdAsync(string userId, string token);
        Task CreateGoalAsync(GoalRequestDtoModel goalToCreate, string token);
        Task UpdateGoalAsync(GoalRequestDtoModel goalToUpdate, string token);
        Task DeleteGoalAsync(int id, string token);
    }
}
