using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Application.IntegrationEvents;
using TransactionalOutBoxPattern.Domain.DomainEvents;
using TransactionalOutBoxPattern.Domain.Repositories;

namespace TransactionalOutBoxPattern.Application.DomainEventHandlers;

internal class EmployeeAddedEventHandler : IDomainEventHandler<EmployeeAdded>
{
    private readonly IIntegrationEventService _integrationEventService;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeAddedEventHandler(
        IDepartmentRepository departmentRepository,
        IIntegrationEventService integrationEventService
    )
    {
        _departmentRepository = departmentRepository;
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(EmployeeAdded @event, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository
            .GetByIdAsync(@event.DepartmentId.Id, cancellationToken);

        if (department is null)
            throw new Exception($"Department Id {@event.DepartmentId.Id} not found!");

        department.AddTotalSalary(@event.Salary.Amount);

        var messageIntegrationEvent = new MessageIntegrationEvent<EmployeeAdded>(@event);
        var emailIntegrationEvent = new EmailIntegrationEvent<EmployeeAdded>(@event);
        _integrationEventService.AddIntegrationEvent(messageIntegrationEvent);
        _integrationEventService.AddIntegrationEvent(emailIntegrationEvent);
    }
}
