using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PennyTracker.Api.Data;
using PennyTracker.Shared.Models;

namespace PennyTracker.Api.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Expense>> GetRangeAsync(DateTime from, DateTime to) =>
            await this.dbContext.Expenses
                .Where(x => x.SpentDate >= from)
                .Where(x => x.SpentDate < to)
                .AsNoTracking()
                .OrderByDescending(x => x.SpentDate)
                .ToListAsync();

        public async Task<Expense> GetAsync(int id) => 
            await this.dbContext.Expenses.FindAsync(id);

        public async Task AddAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            await this.dbContext.Expenses.AddAsync(expense);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            this.dbContext.Expenses.Remove(expense);

            return await this.dbContext.SaveChangesAsync() >= 0;
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            this.dbContext.Expenses.Update(expense);

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.dbContext.Expenses.FindAsync(id) != null
                ? true
                : false;
    }
}
