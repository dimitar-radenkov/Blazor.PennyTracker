using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class ExpenseChartViewModel : IExpenseChartViewModel
    {
        private readonly IExpenseService expenseService;

        public IEnumerable<AmountsByCategory> ExpensesByCategory { get; private set; }

        public ExpenseChartViewModel(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        public async Task OnInitalializedAsync()
        {
            //var expenses = await this.expenseService.GetRangeAsync();
            var expenses = Enumerable.Empty<Expense>();
            var totalSum = expenses.Sum(x => x.Amount);

            this.ExpensesByCategory = expenses.GroupBy(
                    p => p.Category,
                    p => p.Amount,
                    (key, g) =>
                    {
                        var groupSum = g.Sum();

                        return new AmountsByCategory
                        {
                            Category = key.ToString(),
                            Amount = groupSum,
                            Percentage = Math.Round(Convert.ToDouble((groupSum / totalSum) * 100), 2)
                        };
                    })
                    .OrderByDescending(x => x.Amount)
                    .ToList();
        }
    }

    public class AmountsByCategory
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
    }
}
