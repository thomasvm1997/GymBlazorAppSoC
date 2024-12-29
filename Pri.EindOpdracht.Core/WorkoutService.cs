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
    public class WorkoutService : IWorkoutService<Workout>
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkoutService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Workout> GetAll()
        {
            return _dbContext.Workouts.Include(p => p.WorkoutType).Include(p => p.User);
        }
        public async Task<ResultModel<IEnumerable<Workout>>> ListAllAsync()
        {
            var workouts = await GetAll().ToListAsync();
            var resultModel = new ResultModel<IEnumerable<Workout>>();
            if (workouts != null)
            {
                resultModel = new ResultModel<IEnumerable<Workout>>()
                {
                    Data = workouts
                };
            }
            else
            {
                resultModel = new ResultModel<IEnumerable<Workout>>()
                {
                    Errors = new List<string>() { "Could not find workouts" }
                };
            }

            return resultModel;
        }

        public async Task<ResultModel<Workout>> GetByIdAsync(int workoutId)
        {
            var resultModel = new ResultModel<Workout>();
            var workout = await _dbContext.Workouts.Include(p => p.WorkoutType).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == workoutId);

            if (workout == null)
            {
                resultModel = new ResultModel<Workout>();
                resultModel.Errors = new List<string> { "Could not find workout" };
                return resultModel;
            }

            resultModel = new ResultModel<Workout> { Data = workout };

            return resultModel;
        }

        public async Task<ResultModel<IEnumerable<Workout>>> GetWorkoutsByUserAsync(string userId)
        {
            var resultModel = new ResultModel<IEnumerable<Workout>>();

            try
            {
                var workouts = await _dbContext.Workouts
                    .Include(w => w.WorkoutType)
                    .Include(p => p.User)
                    .Where(w => w.UserId == userId) 
                    .ToListAsync();

                if (!workouts.Any())
                {
                    resultModel.Errors.Add("No workouts found for the current user.");
                    return resultModel;
                }

                resultModel.Data = workouts;
            }
            catch (Exception ex)
            {
                resultModel.Errors.Add("An error occurred while retrieving workouts.");
                resultModel.Errors.Add(ex.Message); 
            }

            return resultModel;
        }
        #region CRUD
        public async Task<ResultModel<Workout>> AddAsync(Workout entity)
        {
            var resultModel = new ResultModel<Workout>();


            if (entity == null)
            {
                resultModel = new ResultModel<Workout>();
                resultModel.Errors.Add($"No valid entity given");
                return resultModel;
            }

            //DateTime now = DateTime.UtcNow;
            //entity.CreatedOn = now;
            //entity.LastEditedOn = now;

            _dbContext.Workouts.Add(entity);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(entity).Reference(p => p.User).LoadAsync(); //Met identity moet je dit doen om user in te laden!!!!!!!!!

            resultModel = new ResultModel<Workout> { Data = entity };

            return resultModel;
        }

        public async Task<ResultModel<Workout>> UpdateAsync(Workout entity)
        {
            var resultModel = new ResultModel<Workout>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel = new ResultModel<Workout>();
                resultModel.Errors = result.Errors;
                return resultModel;
            }
            //entity.LastEditedOn = DateTime.UtcNow;

            _dbContext.Workouts.Update(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<Workout> { Data = entity };
            return resultModel;
        }

        public async Task<ResultModel<Workout>> DeleteAsync(Workout entity)
        {
            var resultModel = new ResultModel<Workout>();

            var result = await GetByIdAsync(entity.Id);

            if (result.Success == false)
            {
                resultModel = new ResultModel<Workout>();
                resultModel.Errors = result.Errors;
                return resultModel;
            }

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            resultModel = new ResultModel<Workout> { Data = entity}; 
            return resultModel;
        }
    }

        #endregion
}

