using System;

namespace PennyTracker.Shared.Models.InputBindingModels
{
    public class GetExpensesBindingModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
