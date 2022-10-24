using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

namespace TransactionalOutBoxPattern.Domain.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken token);

    public void Add(Employee employee);
}
