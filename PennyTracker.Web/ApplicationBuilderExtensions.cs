using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PennyTracker.Web.Data;

namespace PennyTracker.Web
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

                    //context.Database.EnsureDeleted();
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
    }
}
