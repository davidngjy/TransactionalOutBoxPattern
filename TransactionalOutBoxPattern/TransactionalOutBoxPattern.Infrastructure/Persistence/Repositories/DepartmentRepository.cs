using Microsoft.EntityFrameworkCore;
using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;
using TransactionalOutBoxPattern.Domain.Repositories;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Repositories;

internal class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public Task<Department?> GetByIdAsync(Guid id, CancellationToken token)
        => _dbContext
            .Set<Department>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(token);

    public void Add(Department department)
        => _dbContext
            .Set<Department>()
            .Add(department);
}
