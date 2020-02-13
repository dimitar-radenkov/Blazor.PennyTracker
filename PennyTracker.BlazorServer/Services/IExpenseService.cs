using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAll();
        Task<Expense> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<Expense> AddAsync(Expense expense);
        Task UpdateAsync(int id, Expense expense);
    }
}
