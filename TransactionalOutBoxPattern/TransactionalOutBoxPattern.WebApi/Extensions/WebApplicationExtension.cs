using TransactionalOutBoxPattern.Infrastructure.Persistence;

namespace TransactionalOutBoxPattern.WebApi.Extensions;

internal static class WebApplicationExtension
{
    public static void MigrateDatabase(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        scope.ServiceProvider
            .GetRequiredService<IDatabaseMigration>()
            .Migrate();
    }
}
