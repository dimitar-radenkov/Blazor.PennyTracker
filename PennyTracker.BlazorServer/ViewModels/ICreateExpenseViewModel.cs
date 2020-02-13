using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface ICreateExpenseViewModel
    {
        Expense Model { get; set; }
        Task OnInitializeAsync(int id = 0);
        Task OnButtonSaveClickAsync();
        void OnButtonCancelClicked();
    }
}
