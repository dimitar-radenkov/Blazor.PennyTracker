using System.Threading.Tasks;
using PennyTracker.BlazorServer.Events;
using PennyTracker.BlazorServer.Services;
using PennyTracker.Shared.Models;

using Prism.Events;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class CreateExpenseComponentViewModel : ICreateExpenseComponentViewModel
    {
        private readonly IDialogService dialogService;
        private readonly IExpenseService expenseService;
        private readonly IEventAggregator eventAggregator;

        public Expense Model { get; set; }
    
        public CreateExpenseComponentViewModel(
            IDialogService dialogService,
            IExpenseService expenseService,
            IEventAggregator eventAggregator)
        {
            this.dialogService = dialogService;
            this.expenseService = expenseService;
            this.eventAggregator = eventAggregator;
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

                this.eventAggregator.GetEvent<TransactionAddedEvent>().Publish(this.Model);
            }
            else //edit
            {
                await this.expenseService.UpdateAsync(
                    this.Model.Id,
                    this.Model.Description,
                    this.Model.Amount,
                    this.Model.Category,
                    this.Model.SpentDate);
            }

            this.dialogService.Close(true);
        }

        public void OnButtonCancelClicked() => this.dialogService.Close(false);
    }
}
