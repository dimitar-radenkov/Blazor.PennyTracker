using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Web.Data;

namespace PennyTracker.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        public Task<Expense> AddAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expense> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Expense> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> UpdateAsync(int id, Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
