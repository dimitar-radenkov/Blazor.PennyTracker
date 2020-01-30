using System.Collections.Generic;

using PennyTracker.Web.Data;

namespace PennyTracker.Web.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> All { get; }
        Expense Get(int id);
        bool Delete(int id);
        Expense Add(Expense expense);
        Expense Update(int id, Expense expense);
    }
}
