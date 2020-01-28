using System;
using PennyTracker.Web.Data;

using Radzen;

namespace PennyTracker.Web.ViewModels
{
    public interface ICreateExpenseViewModel
    {
        Expense Model { get; set; }

        void OnButtonSaveClicked();

        void OnButtonCancelClicked();
    }

    public class CreateExpenseViewModel : ICreateExpenseViewModel
    {
        private readonly DialogService dialogService;

        public Expense Model { get; set; }
    
        public CreateExpenseViewModel(DialogService dialogService)
        {
            this.dialogService = dialogService;
            this.Model = new Expense { SpentDate = DateTime.UtcNow };
        }

        public void OnButtonSaveClicked()
        {
            this.dialogService.Close(true);
        }

        public void OnButtonCancelClicked() => this.dialogService.Close(false);
    }
}
