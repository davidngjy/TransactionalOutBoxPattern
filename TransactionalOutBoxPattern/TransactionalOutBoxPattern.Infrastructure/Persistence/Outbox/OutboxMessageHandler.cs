using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Outbox;

internal class OutboxMessageHandler : IOutboxMessageHandler
{
    private readonly IPublisher _publisher;
    private readonly ApplicationDbContext _dbContext;

    public OutboxMessageHandler(IPublisher publisher, ApplicationDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
    {
        var outboxMessages = await _dbContext
            .Set<OutboxMessage>()
            .Where(x => x.ProcessedOn == null)
            .OrderByDescending(x => x.OccurredOn)
            .Take(10)
            .ToListAsync(cancellationToken);

        foreach (var message in outboxMessages)
        {
            var eventType = Type.GetType(message.Type, true);
            var @event = JsonSerializer.Deserialize(message.Content, eventType!);
            await _publisher.Publish(@event!, cancellationToken);

            message.ProcessedOn = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
