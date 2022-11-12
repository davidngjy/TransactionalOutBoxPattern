namespace TransactionalOutBoxPattern.Domain;

public interface IAuditableEntity
{
    DateTimeOffset CreatedOn { get; }

    DateTimeOffset ModifiedOn { get; }
}
