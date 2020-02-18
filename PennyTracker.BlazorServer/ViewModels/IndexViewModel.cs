using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using PennyTracker.Shared.Models;
using PennyTracker.BlazorServer.Pages;
using PennyTracker.BlazorServer.Services;

using Radzen;
using System;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class IndexViewModel : IIndexViewModel
    {
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public event EventHandler StateChanged;

        public IList<Expense> All { get; set; }

        public IndexViewModel(
            IExpenseService expenseService, 
            NotificationService notificationService,
            IDialogService dialogService)
        {
            this.expenseService = expenseService;
            this.notificationService = notificationService;
            this.dialogService = dialogService;
        }

        public async Task OnInitalializedAsync()
        {
            this.All = await this.expenseService.GetAll();
            this.StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task OnButtonAddClickAsync()
        {
            await this.OpenCreateExpenseDialog(
                title: "Create New Expense",
                id: 0, 
                messageSummary: "Create Expense", 
                messageDetail: "Added Successfully");

            this.All = await this.expenseService.GetAll();
        }

        public async Task OnButtonEditClickAsync(int id)
        {
            await this.OpenCreateExpenseDialog(
                title: "Update Expense",
                id: id,
                messageSummary: "Update Expense",
                messageDetail: "Updated Successfully");

            //this.All = await this.expenseService.GetAll();
        }

        public async Task OnButtonDeleteClickAsync(int id)
        {
            await this.expenseService.DeleteAsync(id);
            var itemToRemove = this.All.FirstOrDefault(x => x.Id == id);

            this.All.Remove(itemToRemove);
        }

        private async Task OpenCreateExpenseDialog(
            string title, 
            int id, 
            string messageSummary, 
            string messageDetail)
        {
            var result = await this.dialogService.OpenAsync<CreateExpense>(
                title: title,
                parameters: new Dictionary<string, object> { { "Id", id } },
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
