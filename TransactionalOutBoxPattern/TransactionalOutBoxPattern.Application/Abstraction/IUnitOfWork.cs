namespace TransactionalOutBoxPattern.Application.Abstraction;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token = default);
}
