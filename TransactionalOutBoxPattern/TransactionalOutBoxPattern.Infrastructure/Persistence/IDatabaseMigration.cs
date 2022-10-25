namespace TransactionalOutBoxPattern.Infrastructure.Persistence;

public interface IDatabaseMigration
{
    Task MigrateAsync(CancellationToken token = default);
}
