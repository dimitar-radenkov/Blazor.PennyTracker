using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface ICreateExpenseViewModel
    {
        Expense Model { get; set; }
        Task OnButtonSaveClickAsync();
        void OnButtonCancelClicked();
    }
}
