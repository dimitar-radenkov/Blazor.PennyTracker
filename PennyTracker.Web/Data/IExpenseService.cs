using System.Collections.Generic;

namespace PennyTracker.Web.Data
{
    public interface IExpenseService
    {
        IEnumerable<Expense> All { get; }
        bool Delete(int id);
        Expense Add(string description, decimal amount);
    }
}
