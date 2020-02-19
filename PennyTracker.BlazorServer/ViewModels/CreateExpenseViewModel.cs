using System.Threading.Tasks;

using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
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

        public async Task OnButtonSaveClickAsync()
        {
            if (this.Model.Id == 0) //new 
            {
                await this.expenseService.AddAsync(
                    this.Model.Description,
                    this.Model.Amount,
                    this.Model.Category,
                    this.Model.SpentDate);
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
