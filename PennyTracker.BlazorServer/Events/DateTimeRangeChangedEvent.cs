using BlazorDateRangePicker;

using Prism.Events;

namespace PennyTracker.BlazorServer.Events
{
    public class DateTimeRangeChangedEvent : PubSubEvent<DateRange>
    {

    }
}
