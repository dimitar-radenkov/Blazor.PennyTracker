using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetRangeAsync(DateTime from, DateTime to);
        Task<Expense> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<Expense> AddAsync(string description, decimal amount, Category category, DateTime spentDate);
        Task UpdateAsync(int id, string description, decimal amount, Category category, DateTime spentDate);
    }
}
