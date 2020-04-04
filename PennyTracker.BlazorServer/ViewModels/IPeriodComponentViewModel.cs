using System.Collections.Generic;

using BlazorDateRangePicker;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IPeriodComponentViewModel
    {
        Dictionary<string, DateRange> Periods { get; }
        DateRange SelectedPeriod { get; }

        void OnPeriodChanged(DateRange selectedItem);
    }
}
