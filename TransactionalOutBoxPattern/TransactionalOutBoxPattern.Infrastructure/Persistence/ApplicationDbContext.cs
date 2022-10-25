using Microsoft.EntityFrameworkCore;
using Polly;
using TransactionalOutBoxPattern.Application.Abstraction;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IDatabaseMigration, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    public Task MigrateAsync(CancellationToken token = default) =>
        Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(5, _ => TimeSpan.FromSeconds(3))
            .ExecuteAsync(Database.MigrateAsync, token);
}
