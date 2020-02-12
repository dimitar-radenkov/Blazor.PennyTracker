using System;
using System.Threading.Tasks;
using PennyTracker.Web.Data;
using PennyTracker.Web.Services;

namespace PennyTracker.Web.ViewModels
{
    public interface ICreateExpenseViewModel
    {
        Expense Model { get; set; }
        Task OnInitializeAsync(int id = 0);
        Task OnButtonSaveClickAsync();
        void OnButtonCancelClicked();
    }

    public class CreateExpenseViewModel : ICreateExpenseViewModel
    {
        private readonly IDialogService dialogService;
        private readonly IExpenseService expenseService;

        public Expense Model { get; set; }
    
        public CreateExpenseViewModel(IDialogService dialogService, IExpenseService expenseService)
        {
            this.dialogService = dialogService;
            this.expenseService = expenseService;
        }

        public async Task OnInitializeAsync(int id = 0)
        {
            var editedItem = await this.expenseService.GetAsync(id);

            this.Model = id == 0 || editedItem == null
                ? new Expense { SpentDate = DateTime.UtcNow }
                : editedItem;
        }

        public async Task OnButtonSaveClickAsync()
        {
            if (this.Model.Id == 0) //new 
            {
                await this.expenseService.AddAsync(this.Model);
            }
            else //edit
            {
                await this.expenseService.UpdateAsync(this.Model.Id, this.Model);
            }

            this.dialogService.Close(true);
        }

        public void OnButtonCancelClicked() => this.dialogService.Close(false);
    }
}
