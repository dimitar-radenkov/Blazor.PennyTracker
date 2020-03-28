using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IExpensesTableViewModel
    {
        IReadOnlyDictionary<string, object> EditButtonAttributes { get; }
        IReadOnlyDictionary<string, object> DeleteButtonAttributes { get; }
        IEnumerable<string> Periods { get; }
        IEnumerable<int> ItemsPerPage { get; }
        string SelectedPeriod { get; set; }
        int SelectedItemsPerPage { get; set; }
        IEnumerable<Expense> Transactions { get; }
        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
        Task OnPeriodChangedAsync(object selectedItem);
        Task OnItemsPerPageChangedAsync(object selectedItem);
    }
}
