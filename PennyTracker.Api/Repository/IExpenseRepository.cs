using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.Api.Repository
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense> GetAsync(int id);
        Task AddAsync(Expense expense);
        Task<bool> UpdateAsync(Expense expense);
        Task<bool> DeleteAsync(Expense expense);
        Task<bool> ExistsAsync(int id);
    }
}
