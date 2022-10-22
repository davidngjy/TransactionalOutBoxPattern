using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

namespace TransactionalOutBoxPattern.Domain.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    public Department? GetById(Guid id);

    public void Add(Department department);
}
