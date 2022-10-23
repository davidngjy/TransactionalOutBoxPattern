using MediatR;

namespace TransactionalOutBoxPattern.Domain.DomainEvents;

public interface IDomainEvent : INotification
{
}
