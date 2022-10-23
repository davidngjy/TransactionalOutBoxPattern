using Microsoft.EntityFrameworkCore;
using TransactionalOutBoxPattern.Application.Abstraction;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
