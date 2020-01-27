using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Web.Data;
using PennyTracker.Web.Pages;

using Radzen;

namespace PennyTracker.ViewModels
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
        private readonly DialogService dialogService;

        public IEnumerable<Expense> All => this.expenseService.All;

        public IndexViewModel(
            IExpenseService expenseService, 
            DialogService dialogService)
        {
            this.expenseService = expenseService;
            this.dialogService = dialogService;
        }
        public async Task OnButtonAddClickAsync()
        {
            await this.dialogService.OpenAsync<CreateExpense>(
                title: "Create New Expense",
                options: new DialogOptions() { Width = "auto", Height = "auto"}) ;
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
