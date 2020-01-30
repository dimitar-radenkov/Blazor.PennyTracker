using System;
using System.Collections.Generic;
using System.Linq;

using PennyTracker.Web.Data;

namespace PennyTracker.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        private List<Expense> expenses;
        public IEnumerable<Expense> All => this.expenses;
        public ExpenseService()
        {
            this.expenses = new List<Expense>()
            {
                new Expense
                {
                    Id = 1,
                    Description = "Diezel",
                    Category = Category.Auto,
                    CreationDate = DateTime.UtcNow.AddDays(-1),
                    SpentDate = DateTime.UtcNow.AddDays(-1),
                    Amount = 150
                },

                new Expense
                {
                    Id = 2,
                    Description = "House",
                    Category = Category.Loans,
                    CreationDate = DateTime.UtcNow,
                    SpentDate = DateTime.UtcNow,
                    Amount = 750
                },

                new Expense
                {
                    Id = 3,
                    Description = "Toys",
                    Category = Category.Childcare,
                    CreationDate = DateTime.UtcNow.AddDays(-2),
                    SpentDate = DateTime.UtcNow.AddDays(-2),
                    Amount = 100
                },
            };
        }

        public Expense Get(int id) =>
            this.expenses.Where(x => x.Id == id).FirstOrDefault();

        public Expense Update(int id, Expense expense)
        {
            try
            {
                var current = this.expenses.Where(x => x.Id == id).FirstOrDefault();
                this.expenses.Remove(current);

                this.expenses.Add(expense);

                return expense;
            }
            catch (Exception)
            {
                //log error
            }

            return null;
        }

        public bool Delete(int id)
        {
            try
            {
                var res = this.expenses.RemoveAll(x => x.Id == id);
            }
            catch (Exception)
            {
                //log errror
                return false;
            }

            return true;
        }

        public Expense Add(Expense expense)
        {
            expense.Id = this.expenses.Max(x => x.Id) + 1;
            expense.CreationDate = DateTime.UtcNow;

            this.expenses.Add(expense);

            return expense;
        }
    }
}
