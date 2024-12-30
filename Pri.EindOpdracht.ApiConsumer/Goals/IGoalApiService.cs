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
        Task<ApiResult> CreateGoalAsync(GoalRequestDtoModel goalToCreate, string token);
        Task<ApiResult> UpdateGoalAsync(GoalRequestDtoModel goalToUpdate, string token);
        Task<ApiResult> DeleteGoalAsync(int id, string token);
    }
}
