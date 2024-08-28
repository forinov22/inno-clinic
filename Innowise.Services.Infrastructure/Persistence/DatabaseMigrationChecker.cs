using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Innowise.Services.Infrastructure.Persistence;

public static class DatabaseMigrationChecker
{
    public static async Task EnsureDatabaseIsFullyMigrated(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ServicesDbContext>();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            throw new Exception("Database is not fully migrated for ServicesDbContext.");
        }
    }
}