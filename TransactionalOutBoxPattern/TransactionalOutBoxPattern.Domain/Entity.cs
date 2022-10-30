using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Domain;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity
    where TId : struct
{
    public TId Id { get; }

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent @event)
        => _domainEvents.Add(@event);

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    protected Entity(TId id) => Id = id;

    public bool Equals(Entity<TId>? other)
        => Equals((object?)other);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not Entity<TId> otherEntity)
            return false;

        return Id.Equals(otherEntity.Id);
    }

    public override int GetHashCode()
        => Id.GetHashCode();

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        => !(left == right);
}
