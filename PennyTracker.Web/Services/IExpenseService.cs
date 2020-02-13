using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.Web.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAll();
        Task<Expense> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Expense> AddAsync(Expense expense);
        Task<Expense> UpdateAsync(int id, Expense expense);
    }
}
