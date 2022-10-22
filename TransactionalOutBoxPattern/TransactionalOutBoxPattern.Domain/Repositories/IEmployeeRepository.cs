using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

namespace TransactionalOutBoxPattern.Domain.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Employee? GetByIdAsync(Guid id);

    public void Add(Employee employee);
}
