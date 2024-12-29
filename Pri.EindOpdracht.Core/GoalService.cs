using Microsoft.EntityFrameworkCore;
using Pri.EindOpdracht.Core.Data;
using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Interfaces;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core
{
    public class GoalService : IGoalService<Goal>
    {
        private readonly ApplicationDbContext _dbContext;
        public GoalService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Goal> GetAll()
        {
            return _dbContext.Goals.Include(p => p.User);
        }
        public async Task<ResultModel<IEnumerable<Goal>>> ListAllAsync()
        {
            var goals = await GetAll().ToListAsync();

            var resultModel = new ResultModel<IEnumerable<Goal>> { Data = goals};

            return resultModel;
        }


        public async Task<ResultModel<Goal>> GetByIdAsync(int goalId)
        {
            var resultModel = new ResultModel<Goal>();

            var goal = await _dbContext.Goals.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == goalId);

            if(goal == null)
            {
                resultModel.Errors = new List<string> {"coudn't find goal" };
                return resultModel;
            }

            resultModel.Data = goal;
            return resultModel;
        }

        public async Task<ResultModel<IEnumerable<Goal>>> GetGoalsByUserAsync(string userId)
        {
            var resultModel = new ResultModel<IEnumerable<Goal>>();

            var goals = await _dbContext.Goals.Include(p => p.User).Where(p => p.UserId == userId).ToListAsync();

            if (goals == null)
            {
                resultModel.Errors = new List<string> { "Couldn't find goals for user" };
                return resultModel;
            }

            resultModel.Data = goals;
            return resultModel;
        }
        #region CRUD
        public async Task<ResultModel<Goal>> AddAsync(Goal entity)
        {
            var resultModel = new ResultModel<Goal>();


            if (entity == null)
            {
                resultModel = new ResultModel<Goal>();
                resultModel.Errors.Add($"No valid entity given");
                return resultModel;
            }

            //DateTime now = DateTime.UtcNow;
            //entity.CreatedOn = now;
            //entity.LastEditedOn = now;

            _dbContext.Goals.Add(entity);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(entity).Reference(p => p.User).LoadAsync(); //Met identity moet je dit doen om user in te laden!!!!!!!!!

            resultModel = new ResultModel<Goal> { Data = entity };

            return resultModel;
        }
        public async Task<ResultModel<Goal>> UpdateAsync(Goal entity)
        {
            var resultModel = new ResultModel<Goal>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel.Errors = result.Errors;
                return resultModel;
            }
            //entity.LastEditedOn = DateTime.UtcNow;

            _dbContext.Goals.Update(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<Goal> { Data = entity };
            return resultModel;
        }
        public async Task<ResultModel<Goal>> DeleteAsync(Goal entity)
        {
            var resultModel = new ResultModel<Goal>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel.Errors = result.Errors;
                return resultModel;
            }

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<Goal> { Data = entity };
            return resultModel;
        }

        #endregion
    }
}
