using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BlazorDateRangePicker;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IExpensesTableViewModel
    {
        event EventHandler RequestedUpdateState;
        IReadOnlyDictionary<string, object> EditButtonAttributes { get; }
        IReadOnlyDictionary<string, object> DeleteButtonAttributes { get; }
        Dictionary<string, DateRange> Periods { get; }
        IEnumerable<int> ItemsPerPage { get; }
        DateRange SelectedPeriod { get; set; }
        int SelectedItemsPerPage { get; set; }
        IEnumerable<Expense> Transactions { get; }
        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
        Task OnPeriodChangedAsync(DateRange selectedItem);
        Task OnItemsPerPageChangedAsync(object selectedItem);
    }
}
