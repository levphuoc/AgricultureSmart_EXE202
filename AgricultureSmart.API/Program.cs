using AgricultureSmart.Repositories.DbAgriContext;
using Microsoft.EntityFrameworkCore;

namespace AgricultureSmart.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            // Apply database migrations
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AgricultureSmartDbContext>();
                    if (context.Database.EnsureCreated())
                    {
                        Console.WriteLine("Database created successfully!");
                    }
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        Console.WriteLine("Applying migrations...");
                        context.Database.Migrate();
                        Console.WriteLine("Migrations applied successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No pending migrations.");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                    Console.WriteLine($"Error creating database: {ex.Message}");
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
