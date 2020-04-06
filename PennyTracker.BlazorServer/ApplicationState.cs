using System;

using BlazorDateRangePicker;

using PennyTracker.BlazorServer.Events;
using PennyTracker.Shared.Extensions;

using Prism.Events;

namespace PennyTracker.BlazorServer
{
    public class ApplicationState
    {
        private DateRange selectedDateRange;

        public static DateRange DefaultDateRange => 
            new DateRange 
            { 
                Start = DateTime.UtcNow.StartOfMonth(), 
                End = DateTime.UtcNow.EndOfMonth() 
            };

        private readonly IEventAggregator eventAggregator;

        public DateRange SelectedDateRange
        {
            get => this.selectedDateRange ?? DefaultDateRange;
            private set => this.selectedDateRange = value;
        }

        public ApplicationState(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>().Subscribe(this.OnDateRangeChanged);

            this.SelectedDateRange = DefaultDateRange;
        }

        private void OnDateRangeChanged(DateRange currentDateRange)
        {
            this.SelectedDateRange = currentDateRange;
        }
    }
}
