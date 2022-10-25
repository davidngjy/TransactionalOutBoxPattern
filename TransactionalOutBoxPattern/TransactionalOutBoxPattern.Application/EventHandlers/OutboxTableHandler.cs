using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Application.EventHandlers;

internal class OutboxTableHandler : IDomainEventHandler<EmployeeAdded>
{
    public async Task Handle(EmployeeAdded @event, CancellationToken cancellationToken)
    {
    }
}
