using MediatR;
using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Application.DomainEventHandlers;

internal interface IDomainEventHandler<in T> : INotificationHandler<T>
    where T : IDomainEvent
{
}
