using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Services.Interfaces
{
    public interface IGoalService<T>
    {
        IQueryable<T> GetAll();
        Task<ResultModel<IEnumerable<T>>> ListAllAsync();
        Task<ResultModel<IEnumerable<T>>> GetGoalsByUserAsync(string userId);
        Task<ResultModel<T>> GetByIdAsync(int goalId);
        Task<ResultModel<T>> UpdateAsync(T entity);
        Task<ResultModel<T>> AddAsync(T entity);
        Task<ResultModel<T>> DeleteAsync(T entity);
    }
}
