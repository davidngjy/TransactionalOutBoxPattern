using TransactionalOutBoxPattern.Domain.DomainEvents;
using TransactionalOutBoxPattern.Domain.Repositories;

namespace TransactionalOutBoxPattern.Application.DomainEventHandlers;

internal class EmployeeSalaryUpdatedEventHandler : IDomainEventHandler<EmployeeSalaryUpdated>
{
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeSalaryUpdatedEventHandler(IDepartmentRepository departmentRepository) =>
        _departmentRepository = departmentRepository;

    public async Task Handle(EmployeeSalaryUpdated @event, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(@event.DepartmentId, cancellationToken);

        if (department is null)
            return;

        var difference = @event.NewSalaryAmount - @event.OldSalaryAmount;

        department.AddTotalSalary(difference);
    }
}
