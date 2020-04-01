using System;
using System.Collections.Generic;

using BlazorDateRangePicker;

using PennyTracker.Shared.Extensions;

using Prism.Events;

namespace PennyTracker.BlazorServer.ViewModels
{
    public interface IPeriodComponentViewModel
    {
        public static readonly DateRange DefaultPeriod = new DateRange 
        {
            Start = DateTime.UtcNow.StartOfMonth(),
            End = DateTime.UtcNow.EndOfMonth()
        };

        Dictionary<string, DateRange> Periods { get; }
        DateRange SelectedPeriod { get; }

        void OnPeriodChanged(DateRange selectedItem);
    }

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
                { "Last Month", new DateRange{ Start = now.StartOfLastMonth(), End = now.StartOfMonth() } },
                { "Last Week", new DateRange{ Start = now.StartOfLastWeek(dayOfWeek), End = now.StartOfWeek(dayOfWeek) } },
                { "Current Week", new DateRange{ Start = now.StartOfWeek(dayOfWeek), End = now.EndOfWeek(dayOfWeek) } },
                { "Current Month",  IPeriodComponentViewModel.DefaultPeriod }
            };
            this.SelectedPeriod = IPeriodComponentViewModel.DefaultPeriod;
            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>().Publish(this.SelectedPeriod);
        }

        public void OnPeriodChanged(DateRange selectedItem)
        {
            this.SelectedPeriod = selectedItem;
            this.eventAggregator.GetEvent<DateTimeRangeChangedEvent>().Publish(selectedItem);
        }
    }

    public class DateTimeRangeChangedEvent : PubSubEvent<DateRange>
    {

    }
}
