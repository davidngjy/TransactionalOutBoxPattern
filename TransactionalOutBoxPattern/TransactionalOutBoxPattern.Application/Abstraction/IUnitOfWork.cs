namespace TransactionalOutBoxPattern.Application.Abstraction;

public interface IUnitOfWork
{
    public void SetAsQueryMode();

    public Task<int> SaveChangesAsync(CancellationToken token = default);
}
