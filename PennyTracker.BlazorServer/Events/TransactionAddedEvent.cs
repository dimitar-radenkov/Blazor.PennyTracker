using PennyTracker.Shared.Models;

using Prism.Events;

namespace PennyTracker.BlazorServer.Events
{
    public class TransactionAddedEvent : PubSubEvent<Expense>
    {
    }
}
