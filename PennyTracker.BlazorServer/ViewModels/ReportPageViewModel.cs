using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PennyTracker.BlazorServer.Events;
using PennyTracker.BlazorServer.Services;

using Prism.Events;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class ReportPageViewModel : IReportPageViewModel
    {
        private readonly ApplicationState applicationState;
        private readonly IEventAggregator eventAggregator;
        private readonly IExpenseService expenseService;

        public event EventHandler RequestedUpdateState;

        public IEnumerable<AmountsByCategory> ExpensesByCategory { get; private set; }

        public ReportPageViewModel(
            ApplicationState applicationState,
            IEventAggregator eventAggregator,
            IExpenseService expenseService)
        {
            this.applicationState = applicationState;
            this.eventAggregator = eventAggregator;
            this.expenseService = expenseService;

            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>()
                .Subscribe(async (dataRange) =>
                {
                    await this.UpdateData(dataRange.Start.UtcDateTime, dataRange.End.UtcDateTime);
                });
        }

        public async Task OnInitalializedAsync()
        {
            var currentDateRange = this.applicationState.SelectedDateRange;

            await this.UpdateData(
                currentDateRange.Start.UtcDateTime,
                currentDateRange.End.UtcDateTime);
        }

        private async Task UpdateData(DateTime start, DateTime end)
        {
            var expenses = await this.expenseService.GetRangeAsync(start, end);

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

            this.RequestedUpdateState?.Invoke(this, EventArgs.Empty);
        }
    }

    public class AmountsByCategory
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
    }
}
