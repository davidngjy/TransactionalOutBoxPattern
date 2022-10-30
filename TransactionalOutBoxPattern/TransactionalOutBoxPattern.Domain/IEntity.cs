using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Domain;

public interface IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
