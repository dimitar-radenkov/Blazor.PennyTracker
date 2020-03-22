using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PennyTracker.Api.Repository;
using PennyTracker.Shared.Extensions;
using PennyTracker.Shared.Models;
using PennyTracker.Shared.Models.InputBindingModels;

namespace PennyTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpensesController(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        // GET: api/Expenses
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(long fromUnixTime, long toUnixTime)
        {
            var fromDateTime = fromUnixTime.ToDateTime();
            var toDateTime = toUnixTime.ToDateTime();

            return this.Ok(await this.expenseRepository.GetRangeAsync(fromDateTime, toDateTime));
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await this.expenseRepository.GetAsync(id);

            if (expense == null)
            {
                return this.NotFound();
            }

            return this.Ok(expense);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, UpdateExpenseBindingModel model)
        {
            var expense = await this.expenseRepository.GetAsync(id);
            if (expense == null)
            {
                return this.NotFound();
            }

            expense.Amount = model.Amount;
            expense.Description = model.Description;
            expense.Category = model.Category;
            expense.SpentDate = model.SpentDate;

            if(!await this.expenseRepository.UpdateAsync(expense))
            {
                if(!await this.expenseRepository.ExistsAsync(id))
                {
                    return this.NotFound();
                }
            }

            return this.NoContent();
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(AddExpenseBindingModel model)
        {
            var expense = new Expense
            {
                Description = model.Description,
                Amount = model.Amount,
                Category = model.Category,
                SpentDate = model.SpentDate,
                CreationDate = DateTime.UtcNow,
            };

            await this.expenseRepository.AddAsync(expense);

            return this.CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expense = await this.expenseRepository.GetAsync(id);
            if (expense == null)
            {
                return this.NotFound();
            }

            await this.expenseRepository.DeleteAsync(expense);

            return this.NoContent();
        }
    }
}
