using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface ITransactionsComponentViewModel
    {
        event EventHandler RequestedUpdateState;

        IReadOnlyDictionary<string, object> EditButtonAttributes { get; }
        IReadOnlyDictionary<string, object> DeleteButtonAttributes { get; }

        IEnumerable<int> ItemsPerPage { get; }
        int SelectedItemsPerPage { get; set; }
        IEnumerable<Expense> Transactions { get; }

        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
        Task OnItemsPerPageChangedAsync(object selectedItem);
    }
}
