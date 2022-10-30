using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.DomainEvents;
using TransactionalOutBoxPattern.Domain.Repositories;

namespace TransactionalOutBoxPattern.Application.EventHandlers;

internal class EmployeeAddedEventHandler : IDomainEventHandler<EmployeeAdded>
{
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeAddedEventHandler(IDepartmentRepository departmentRepository)
        => _departmentRepository = departmentRepository;

    public async Task Handle(EmployeeAdded @event, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository
            .GetByIdAsync(@event.DepartmentId.Id, cancellationToken);

        if (department is null)
            throw new Exception($"Department Id {@event.DepartmentId.Id} not found!");

        department.IncreaseTotalSalary(@event.Salary.Amount);
    }
}
