using System;
using System.ComponentModel.DataAnnotations;

namespace PennyTracker.Shared.Models.InputBindingModels
{
    public class AddExpenseBindingModel
    {
        [Required]
        [StringLength(128, ErrorMessage = "Max length is 128 symbols")]
        public string Description { get; set; }

        [Required]
        [Range(1, 1_000_000, ErrorMessage = "Amount must be between 1 and 1 000 000")]
        public decimal Amount { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public DateTime SpentDate { get; set; }
    }
}
