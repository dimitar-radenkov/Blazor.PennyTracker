using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PennyTracker.Api.Data;
using PennyTracker.Shared.Models;

namespace PennyTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ExpensesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await this.dbContext.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await this.dbContext.Expenses.FindAsync(id);

            if (expense == null)
            {
                return this.NotFound();
            }

            return expense;
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

            this.dbContext.Entry(expense).State = EntityState.Modified;

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ExpenseExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            this.dbContext.Expenses.Add(expense);
            await this.dbContext.SaveChangesAsync();

            return this.CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expense = await this.dbContext.Expenses.FindAsync(id);
            if (expense == null)
            {
                return this.NotFound();
            }

            this.dbContext.Expenses.Remove(expense);
            await this.dbContext.SaveChangesAsync();

            return expense;
        }

        private bool ExpenseExists(int id)
        {
            return this.dbContext.Expenses.Any(e => e.Id == id);
        }
    }
}
