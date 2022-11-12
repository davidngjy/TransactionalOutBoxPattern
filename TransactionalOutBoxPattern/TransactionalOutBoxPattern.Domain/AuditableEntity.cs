namespace TransactionalOutBoxPattern.Domain;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
    where TId : struct
{
    public DateTimeOffset CreatedOn { get; } = default;

    public DateTimeOffset ModifiedOn { get; } = default;

    protected AuditableEntity(TId id) : base(id)
    {
    }
}
