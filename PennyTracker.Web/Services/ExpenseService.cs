using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PennyTracker.Web.Data;

namespace PennyTracker.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext dbContext;

        public ExpenseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync() => await this.dbContext.Expenses.AsNoTracking().ToListAsync();

        public async Task<Expense> GetAsync(int id) => await this.dbContext.Expenses.FindAsync(id);

        public async Task<Expense> UpdateAsync(int id, Expense expense)
        {
            try
            {
                var current = this.dbContext.Expenses.Find(id);

                current.Amount = expense.Amount;
                current.Category = expense.Category;
                current.Description = expense.Description;
                current.SpentDate = expense.SpentDate;


                this.dbContext.Expenses.Update(current);
                await this.dbContext.SaveChangesAsync();

                return current;
            }
            catch (Exception)
            {
                //log error
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var expense = await this.dbContext.Expenses.FindAsync(id);
                this.dbContext.Expenses.Remove(expense);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                //log errror
                return false;
            }

            return true;
        }

        public async Task<Expense> AddAsync(Expense expense)
        {
            try
            {
                await this.dbContext.Expenses.AddAsync(expense);
                await this.dbContext.SaveChangesAsync();

                return expense;
            }
            catch (Exception)
            {
                //log error
            }

            return null;
        }
    }
}
