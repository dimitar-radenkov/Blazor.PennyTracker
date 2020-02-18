using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IIndexViewModel
    {
        event EventHandler StateChanged;
        IList<Expense> All { get; }
        Task OnInitalializedAsync();
        Task OnButtonAddClickAsync();
        Task OnButtonEditClickAsync(int id);
        Task OnButtonDeleteClickAsync(int id);
    }
}
