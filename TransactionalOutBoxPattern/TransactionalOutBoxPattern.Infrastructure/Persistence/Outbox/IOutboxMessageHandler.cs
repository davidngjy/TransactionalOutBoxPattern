namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Outbox;

public interface IOutboxMessageHandler
{
    Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken);
}
