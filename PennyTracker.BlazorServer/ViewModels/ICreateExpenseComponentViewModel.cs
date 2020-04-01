using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface ICreateExpenseComponentViewModel
    {
        Expense Model { get; set; }
        Task OnButtonSaveClickAsync();
        void OnButtonCancelClicked();
    }
}
