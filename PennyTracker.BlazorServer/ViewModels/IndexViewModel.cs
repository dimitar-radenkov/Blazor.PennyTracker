using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PennyTracker.BlazorServer.Pages;
using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Models;

using Radzen;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class IndexViewModel : IIndexViewModel
    {
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public event EventHandler StateChanged;

        public IList<Expense> All { get; set; }

        public IReadOnlyDictionary<string, object> EditButtonAttributes => new Dictionary<string, object>() { { "title", "Edit" } };
        public IReadOnlyDictionary<string, object> DeleteButtonAttributes => new Dictionary<string, object>() { { "title", "Delete" } };

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
                model: new Expense { SpentDate = DateTime.UtcNow }, 
                messageSummary: "Create Expense", 
                messageDetail: "Added Successfully");

            this.All = await this.expenseService.GetAll();
        }

        public async Task OnButtonEditClickAsync(int id)
        {
            var model = await this.expenseService.GetAsync(id);

            await this.OpenCreateExpenseDialog(
                title: "Update Expense",
                model: model,
                messageSummary: "Update Expense",
                messageDetail: "Updated Successfully");

            this.All = await this.expenseService.GetAll();
        }

        public async Task OnButtonDeleteClickAsync(int id)
        {
            await this.expenseService.DeleteAsync(id);
            var itemToRemove = this.All.FirstOrDefault(x => x.Id == id);

            this.All.Remove(itemToRemove);
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
