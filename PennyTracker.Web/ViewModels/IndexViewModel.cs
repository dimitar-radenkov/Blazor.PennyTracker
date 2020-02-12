using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Web.Data;
using PennyTracker.Web.Pages;
using PennyTracker.Web.Services;

using Radzen;

namespace PennyTracker.Web.ViewModels
{
    public interface IIndexViewModel
    {
        IEnumerable<Expense> All { get; }
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
    }

    public class IndexViewModel : IIndexViewModel
    {
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public IEnumerable<Expense> All => this.expenseService.GetAll();

        public IndexViewModel(
            IExpenseService expenseService, 
            NotificationService notificationService,
            IDialogService dialogService)
        {
            this.expenseService = expenseService;
            this.notificationService = notificationService;
            this.dialogService = dialogService;
        }

        public async Task OnButtonAddClickAsync()
        {
            await this.OpenCreateExpenseDialog(
                title: "Create New Expense",
                id: 0, 
                messageSummary: "Create Expense", 
                messageDetail: "Added Successfully");
        }

        public async Task OnButtonEditClickAsync(int id)
        {
            await this.OpenCreateExpenseDialog(
                title: "Edit Expense",
                id: id,
                messageSummary: "Edit Expense",
                messageDetail: "Added Successfully");
        }

        public async Task OnButtonDeleteClickAsync(int id)
        {
            await this.expenseService.DeleteAsync(id);
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
