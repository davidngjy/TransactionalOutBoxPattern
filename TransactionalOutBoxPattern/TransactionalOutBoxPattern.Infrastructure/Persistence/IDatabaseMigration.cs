namespace TransactionalOutBoxPattern.Infrastructure.Persistence;

public interface IDatabaseMigration
{
    void Migrate();
}
