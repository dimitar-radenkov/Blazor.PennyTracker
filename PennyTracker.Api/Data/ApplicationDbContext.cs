using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PennyTracker.Shared.Models;

namespace PennyTracker.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
