using System;
using System.Collections.Generic;
using System.Linq;

namespace PennyTracker.Web.Data
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

        public Expense Add(string description, decimal amount)
        {
            var item = new Expense
            {
                Id = this.expenses.Max(x => x.Id) + 1,
                Description = description,
                Amount = amount,
            };

            this.expenses.Add(item);

            return item;
        }
    }
}
