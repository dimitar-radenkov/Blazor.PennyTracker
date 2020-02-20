using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PennyTracker.Api.Repository;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return this.Ok(await this.expenseRepository.GetAllAsync());
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return this.BadRequest();
            }

            if(!await this.expenseRepository.UpdateAsync(expense))
            {
                if(!await this.expenseRepository.ExistsAsync(id))
                {
                    return this.NotFound();
                }
            }

            return this.NoContent();
        }

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
