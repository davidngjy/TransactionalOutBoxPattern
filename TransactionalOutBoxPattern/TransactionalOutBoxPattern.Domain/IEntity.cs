using MediatR;

namespace TransactionalOutBoxPattern.Domain;

public interface IEntity
{
    IReadOnlyList<INotification> DomainEvents { get; }

    void ClearDomainEvents();
}
