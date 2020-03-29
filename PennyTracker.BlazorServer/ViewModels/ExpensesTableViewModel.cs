using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorDateRangePicker;

using PennyTracker.BlazorServer.Events;
using PennyTracker.BlazorServer.Pages;
using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Extensions;
using PennyTracker.Shared.Models;

using Prism.Events;

using Radzen;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class ExpensesTableViewModel : IExpensesTableViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public event EventHandler RequestedUpdateState;

        public IEnumerable<Expense> Transactions { get; set; }

        public Dictionary<string, DateRange> Periods { get; }

        public IEnumerable<int> ItemsPerPage { get; }

        public DateRange SelectedPeriod { get; set; }

        public int SelectedItemsPerPage { get; set; }

        public IReadOnlyDictionary<string, object> EditButtonAttributes => new Dictionary<string, object>() { { "title", "Edit" } };
        public IReadOnlyDictionary<string, object> DeleteButtonAttributes => new Dictionary<string, object>() { { "title", "Delete" } };

        public ExpensesTableViewModel(
            IEventAggregator eventAggregator,
            IExpenseService expenseService, 
            NotificationService notificationService,
            IDialogService dialogService)
        {
            this.eventAggregator = eventAggregator;
            this.expenseService = expenseService;
            this.notificationService = notificationService;
            this.dialogService = dialogService;

            this.eventAggregator.GetEvent<AddTransactionEvent>()
                .Subscribe(async () => await this.OnButtonAddClickAsync());

            this.ItemsPerPage = new List<int> { 5, 8, 10, 50, 100 };
            this.SelectedItemsPerPage = this.ItemsPerPage.First();

            var now = DateTime.UtcNow;
            var dayOfWeek = DayOfWeek.Monday;
            this.Periods = new Dictionary<string, DateRange>
            {
                { "Last Month", new DateRange{ Start = now.StartOfLastMonth(), End = now.StartOfMonth() } },
                { "Last Week", new DateRange{ Start = now.StartOfLastWeek(dayOfWeek), End = now.EndOfLastWeek(dayOfWeek) } },
                { "Current Week", new DateRange{ Start = now.StartOfWeek(dayOfWeek), End = now.EndOfWeek(dayOfWeek) } },
                { "Current Month", new DateRange{ Start = now.StartOfMonth(), End = now.EndOfMonth() } },
            };
            this.SelectedPeriod = this.Periods.Last().Value;
        }

        public async Task OnInitalializedAsync()
        {
            await this.OnPeriodChangedAsync(this.SelectedPeriod);
        }

        public async Task OnButtonAddClickAsync()
        {
            await this.OpenCreateExpenseDialog(
                title: "Create New Expense",
                model: new Expense { SpentDate = DateTime.UtcNow }, 
                messageSummary: "Create Expense", 
                messageDetail: "Added Successfully");

            await this.OnPeriodChangedAsync(this.SelectedPeriod);

            this.RequestedUpdateState?.Invoke(this, EventArgs.Empty);
        }

        public async Task OnButtonEditClickAsync(int id)
        {
            var model = await this.expenseService.GetAsync(id);

            await this.OpenCreateExpenseDialog(
                title: "Update Expense",
                model: model,
                messageSummary: "Update Expense",
                messageDetail: "Updated Successfully");

            await this.OnPeriodChangedAsync(this.SelectedPeriod);
        }

        public async Task OnButtonDeleteClickAsync(int id)
        {
            await this.expenseService.DeleteAsync(id);
            this.Transactions = this.Transactions.Where(x => x.Id != id);
        }

        public async Task OnPeriodChangedAsync(DateRange range)
        {
            this.Transactions = await this.expenseService.GetRangeAsync(range.Start.UtcDateTime, range.End.UtcDateTime);
            this.SelectedPeriod = range;
        }

        public async Task OnItemsPerPageChangedAsync(object args)
        {
            //This seems to be bug in DataGrid component.
            //It needs to change underlaying collection to trigger rerendering
            this.Transactions = this.Transactions.OrderBy(x => x.SpentDate);

            await Task.FromResult(this.Transactions);
        }

        private async Task OpenCreateExpenseDialog(
            string title, 
            Expense model, 
            string messageSummary, 
            string messageDetail)
        {
            var result = await this.dialogService.OpenAsync<CreateExpense>(
                title: title,
                parameters: new Dictionary<string, object> { { "model", model } },
                options: new DialogOptions() { Width = "500px", Height = "auto", Left = "calc(50% - 250px)" });

            if (result)
            {
                this.notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = messageSummary,
                    Detail = messageDetail,
                    Duration = 4000
                });
            }
        }
    }
}
