using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PennyTracker.Api.Data;
using PennyTracker.Shared.Models;

namespace PennyTracker.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred migrating the DB.");
                }
            }
        }

        public static void SeedIfEmptyDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();

                    if (!context.Expenses.Any())
                    {
                        context.Expenses.Add(new Expense { Description = "diesel", Category = Category.Auto, Amount = 150M, CreationDate = DateTime.UtcNow, SpentDate = DateTime.UtcNow });
                        context.Expenses.Add(new Expense { Description = "diesel", Category = Category.Auto, Amount = 120M, CreationDate = DateTime.UtcNow.AddDays(-1), SpentDate = DateTime.UtcNow.AddDays(-1) });
                        context.Expenses.Add(new Expense { Description = "diesel", Category = Category.Auto, Amount = 122M, CreationDate = DateTime.UtcNow.AddDays(-4), SpentDate = DateTime.UtcNow.AddDays(-4) });
                        context.Expenses.Add(new Expense { Description = "food", Category = Category.Grocery, Amount = 350M, CreationDate = DateTime.UtcNow, SpentDate = DateTime.UtcNow });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 25, CreationDate = DateTime.UtcNow, SpentDate = DateTime.UtcNow });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 22, CreationDate = DateTime.UtcNow.AddDays(-1), SpentDate = DateTime.UtcNow.AddDays(-1) });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 14, CreationDate = DateTime.UtcNow.AddDays(-2), SpentDate = DateTime.UtcNow.AddDays(-2) });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 6, CreationDate = DateTime.UtcNow.AddDays(-3), SpentDate = DateTime.UtcNow.AddDays(-3) });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 9, CreationDate = DateTime.UtcNow.AddDays(-4), SpentDate = DateTime.UtcNow.AddDays(-4) });
                        context.Expenses.Add(new Expense { Description = "lunch", Category = Category.EatingOut, Amount = 10, CreationDate = DateTime.UtcNow.AddDays(-5), SpentDate = DateTime.UtcNow.AddDays(-5) });
                        context.Expenses.Add(new Expense { Description = "pharmacy", Category = Category.Healthcare, Amount = 150M, CreationDate = DateTime.UtcNow, SpentDate = DateTime.UtcNow });
                        context.Expenses.Add(new Expense { Description = "mortgage", Category = Category.Loans, Amount = 750, CreationDate = DateTime.UtcNow, SpentDate = DateTime.UtcNow });

                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }


    }
}
