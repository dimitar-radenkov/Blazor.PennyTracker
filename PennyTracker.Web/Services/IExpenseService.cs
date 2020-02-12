using System.Collections.Generic;
using System.Threading.Tasks;
using PennyTracker.Web.Data;

namespace PennyTracker.Web.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Expense> AddAsync(Expense expense);
        Task<Expense> UpdateAsync(int id, Expense expense);
    }
}
