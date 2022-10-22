namespace TransactionalOutBoxPattern.Application.Abstraction;

public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken token = default);
}
