using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Services.Interfaces
{
    public interface IWorkoutService<T>
    {
        IQueryable<Workout> GetAll();
        Task<ResultModel<IEnumerable<T>>> ListAllAsync();
        Task<ResultModel<T>> GetByIdAsync(int workoutId);
        Task<ResultModel<IEnumerable<T>>> GetWorkoutsByUserAsync(string userId);
        Task<ResultModel<T>> UpdateAsync(T entity);
        Task<ResultModel<T>> AddAsync(T entity);
        Task<ResultModel<T>> DeleteAsync(T entity);
    }
}
