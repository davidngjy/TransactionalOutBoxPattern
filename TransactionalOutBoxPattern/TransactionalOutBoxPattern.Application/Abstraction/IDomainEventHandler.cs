using MediatR;
using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Application.Abstraction;

internal interface IDomainEventHandler<in T> : INotificationHandler<T>
    where T : IDomainEvent
{
}
