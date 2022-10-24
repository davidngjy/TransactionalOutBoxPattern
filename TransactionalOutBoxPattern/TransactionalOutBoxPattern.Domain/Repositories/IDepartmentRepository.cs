using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

namespace TransactionalOutBoxPattern.Domain.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    public Task<Department?> GetByIdAsync(Guid id, CancellationToken token);

    public void Add(Department department);
}
