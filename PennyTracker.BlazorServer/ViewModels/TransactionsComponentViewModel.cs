using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorDateRangePicker;

using PennyTracker.BlazorServer.Components;
using PennyTracker.BlazorServer.Events;
using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Models;

using Prism.Events;

using Radzen;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class TransactionsComponentViewModel : ITransactionsComponentViewModel
    {
        private readonly ApplicationState applicationState;
        private readonly IEventAggregator eventAggregator;
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public event EventHandler RequestedUpdateState;

        public IEnumerable<Expense> Transactions { get; set; }

        public IEnumerable<int> ItemsPerPage { get; }

        public int SelectedItemsPerPage { get; set; }

        public IReadOnlyDictionary<string, object> EditButtonAttributes => new Dictionary<string, object>() 
        { 
            { "title", "Edit" },
        };
        public IReadOnlyDictionary<string, object> DeleteButtonAttributes => new Dictionary<string, object>() 
        { 
            { "title", "Delete" },
        };

        public TransactionsComponentViewModel(
            ApplicationState applicationState,
            IEventAggregator eventAggregator,
            IExpenseService expenseService, 
            NotificationService notificationService,
            IDialogService dialogService)
        {
            this.applicationState = applicationState;
            this.eventAggregator = eventAggregator;
            this.expenseService = expenseService;
            this.notificationService = notificationService;
            this.dialogService = dialogService;

            this.eventAggregator.GetEvent<ButtonAddClickedEvent>()
                .Subscribe(async () => await this.OnButtonAddClickAsync());

            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>()
                .Subscribe(async (dataRange) =>  await this.OnPeriodChangedAsync(dataRange));

            this.ItemsPerPage = new List<int> { 10, 25, 50, 100 };
            this.SelectedItemsPerPage = this.ItemsPerPage.First();
        }

        public async Task OnInitalializedAsync()
        {
            await this.OnPeriodChangedAsync(this.applicationState.SelectedDateRange);         
        }

        public async Task OnButtonAddClickAsync()
        {
            await this.OpenCreateExpenseDialog(
                title: "Create New Expense",
                model: new Expense { SpentDate = DateTime.UtcNow }, 
                messageSummary: "Create Expense", 
                messageDetail: "Added Successfully");

            await this.OnPeriodChangedAsync(this.applicationState.SelectedDateRange);
        }

        public async Task OnButtonEditClickAsync(int id)
        {
            var model = await this.expenseService.GetAsync(id);

            await this.OpenCreateExpenseDialog(
                title: "Update Expense",
                model: model,
                messageSummary: "Update Expense",
                messageDetail: "Updated Successfully");

            await this.OnPeriodChangedAsync(this.applicationState.SelectedDateRange);
        }

        public async Task OnButtonDeleteClickAsync(int id)
        {
            await this.expenseService.DeleteAsync(id);
            this.Transactions = this.Transactions.Where(x => x.Id != id);
        }

        public async Task OnItemsPerPageChangedAsync(object args)
        {
            //This seems to be bug in DataGrid component.
            //It needs to change underlaying collection to trigger rerendering
            this.Transactions = this.Transactions.OrderBy(x => x.SpentDate);

            await Task.FromResult(this.Transactions);
        }

        public async Task OnPeriodChangedAsync(DateRange range)
        {
            this.Transactions = await this.expenseService.GetRangeAsync(
                range.Start.UtcDateTime,
                range.End.UtcDateTime);

            this.RequestedUpdateState?.Invoke(this, EventArgs.Empty);
        }

        private async Task OpenCreateExpenseDialog(
            string title, 
            Expense model, 
            string messageSummary, 
            string messageDetail)
        {
            var result = await this.dialogService.OpenAsync<CreateExpenseComponent>(
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
