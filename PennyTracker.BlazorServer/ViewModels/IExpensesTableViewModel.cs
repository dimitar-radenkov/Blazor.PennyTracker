using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IExpensesTableViewModel
    {
        event EventHandler StateChanged;

        IReadOnlyDictionary<string, object> EditButtonAttributes { get; }
        IReadOnlyDictionary<string, object> DeleteButtonAttributes { get; }
        IEnumerable<string> Periods { get; }
        string SelectedPeriod { get; set; }
        IList<Expense> Transactions { get; }
        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
        Task OnPeriodChangedAsync(object selectedItem);
    }
}
