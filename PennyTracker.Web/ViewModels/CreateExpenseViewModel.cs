using System;

using PennyTracker.Web.Data;
using PennyTracker.Web.Services;

namespace PennyTracker.Web.ViewModels
{
    public interface ICreateExpenseViewModel
    {
        Expense Model { get; set; }
        void OnInitialized(int id = 0);
        void OnButtonSaveClicked();
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

        public void OnInitialized(int id = 0)
        {
            var editedItem = this.expenseService.Get(id);

            this.Model = id == 0 || editedItem == null
                ? new Expense { SpentDate = DateTime.UtcNow }
                : editedItem;
        }

        public void OnButtonSaveClicked()
        {
            if (this.Model.Id == 0)
            {
                this.expenseService.Add(this.Model);
            }
            else
            {
                this.expenseService.Update(this.Model.Id, this.Model);
            }

            this.dialogService.Close(true);
        }

        public void OnButtonCancelClicked() => this.dialogService.Close(false);
    }
}
