using System;

namespace PennyTracker.Web.Data
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime SpentDate { get; set; }
    }
}
