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
    public class WorkoutTypeService : IWorkoutTypeService<WorkoutType>
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkoutTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<WorkoutType> GetAll()
        {
            return _dbContext.WorkoutTypes.Include(p => p.Workouts);
        }
        public async Task<ResultModel<IEnumerable<WorkoutType>>> ListAllAsync()
        {
            var workoutTypes = await GetAll().ToListAsync();

            var resultModel = new ResultModel<IEnumerable<WorkoutType>> { Data = workoutTypes };

            return resultModel;
        }
        public async Task<ResultModel<WorkoutType>> GetByIdAsync(int workoutTypeId)
        {
            var resultModel = new ResultModel<WorkoutType>();

            var goal = await _dbContext.WorkoutTypes.Include(p => p.Workouts).FirstOrDefaultAsync(p => p.Id == workoutTypeId);

            if (goal == null)
            {
                resultModel.Errors = new List<string> { "coudn't find workout type" };
                return resultModel;
            }

            resultModel.Data = goal;
            return resultModel;
        }

        #region CRUD
        public async Task<ResultModel<WorkoutType>> AddAsync(WorkoutType entity)
        {
            var resultModel = new ResultModel<WorkoutType>();


            if (entity == null)
            {
                resultModel.Errors.Add($"No valid entity given");
                return resultModel;
            }

            //DateTime now = DateTime.UtcNow;
            //entity.CreatedOn = now;
            //entity.LastEditedOn = now;

            _dbContext.WorkoutTypes.Add(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<WorkoutType> { Data = entity };

            return resultModel;
        }
        public async Task<ResultModel<WorkoutType>> UpdateAsync(WorkoutType entity)
        {
            var resultModel = new ResultModel<WorkoutType>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel.Errors = result.Errors;
                return resultModel;
            }
            //entity.LastEditedOn = DateTime.UtcNow;

            _dbContext.WorkoutTypes.Update(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<WorkoutType> { Data = entity };
            return resultModel;
        }

        public async Task<ResultModel<WorkoutType>> DeleteAsync(WorkoutType entity)
        {
            var resultModel = new ResultModel<WorkoutType>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel.Errors = result.Errors;
                return resultModel;
            }

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<WorkoutType> { Data = entity };
            return resultModel;
        }
        #endregion
    }
}
