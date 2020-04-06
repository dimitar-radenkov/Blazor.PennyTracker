using System;
using System.Collections.Generic;
using System.Linq;

using BlazorDateRangePicker;

using PennyTracker.BlazorServer.Events;
using PennyTracker.Shared.Extensions;

using Prism.Events;

namespace PennyTracker.BlazorServer.ViewModels
{
    public class PeriodComponentViewModel : IPeriodComponentViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public Dictionary<string, DateRange> Periods { get; }

        public DateRange SelectedPeriod { get; private set; }

        public PeriodComponentViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            var now = DateTime.UtcNow;
            var dayOfWeek = DayOfWeek.Monday;
            this.Periods = new Dictionary<string, DateRange>
            {
                { "Last Month", new DateRange { Start = now.StartOfLastMonth(), End = now.StartOfMonth() } },
                { "Last Week", new DateRange { Start = now.StartOfLastWeek(dayOfWeek), End = now.StartOfWeek(dayOfWeek) } },
                { "Current Week", new DateRange { Start = now.StartOfWeek(dayOfWeek), End = now.EndOfWeek(dayOfWeek) } },
                { "Current Month",  ApplicationState.DefaultDateRange }
            };

            this.OnPeriodChanged(this.Periods.Last().Value);
        }

        public void OnPeriodChanged(DateRange selectedItem)
        {
            this.SelectedPeriod = selectedItem;
            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>().Publish(selectedItem);
        }
    }
}
