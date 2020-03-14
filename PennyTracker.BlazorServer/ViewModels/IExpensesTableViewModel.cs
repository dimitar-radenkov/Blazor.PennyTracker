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
        IList<Expense> All { get; }
        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
    }
}
