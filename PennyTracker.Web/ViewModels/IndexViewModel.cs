using PennyTracker.Web.Data;
using PennyTracker.Web.Pages;
using PennyTracker.Web.Services;

using Radzen;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PennyTracker.Web.ViewModels
{
    public interface IIndexViewModel
    {
        IEnumerable<Expense> All { get; }
        Task OnButtonAddClickAsync();
        void OnButtonEditClick(int id);
        void OnButtonDeleteClick(int id);
    }

    public class IndexViewModel : IIndexViewModel
    {
        private readonly IExpenseService expenseService;
        private readonly NotificationService notificationService;
        private readonly IDialogService dialogService;

        public IEnumerable<Expense> All => this.expenseService.All;

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
            var result = await this.dialogService.OpenAsync<CreateExpense>(
                title: "Create New Expense",
                options: new DialogOptions() { Width = "500px", Height = "auto", Left = "calc(50% - 250px)"});

            if (result)
            {
                this.notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Create Expense",
                    Detail = "Added Sucessfully",
                    Duration = 4000
                });
            }
        }

        public void OnButtonEditClick(int id)
        {

        }

        public void OnButtonDeleteClick(int id)
        {
            this.expenseService.Delete(id);
        }


    }
}
