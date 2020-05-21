using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IReportPageViewModel
    {
        event EventHandler RequestedUpdateState;

        IEnumerable<CategoryAndAmount> ExpensesByCategory { get; }

        Task OnInitalializedAsync();
    }
}
