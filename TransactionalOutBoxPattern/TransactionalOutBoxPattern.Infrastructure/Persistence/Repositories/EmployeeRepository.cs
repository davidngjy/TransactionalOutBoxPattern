using Microsoft.EntityFrameworkCore;
using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;
using TransactionalOutBoxPattern.Domain.Repositories;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken token)
        => _dbContext
            .Set<Employee>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(token);

    public void Add(Employee employee)
        => _dbContext
            .Set<Employee>()
            .Add(employee);
}
