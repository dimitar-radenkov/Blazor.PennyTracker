using System.Collections.Generic;
using System.Threading.Tasks;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IExpenseChartViewModel
    {
        IEnumerable<AmountsByCategory> ExpensesByCategory { get; }

        Task OnInitalializedAsync();
    }
}
