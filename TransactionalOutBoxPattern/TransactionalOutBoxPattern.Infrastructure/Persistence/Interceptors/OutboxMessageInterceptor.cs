using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using TransactionalOutBoxPattern.Domain;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Outbox;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;

internal class OutboxMessageInterceptor : SaveChangesInterceptor
{
    private readonly ApplicationDbContext _dbContext;

    public OutboxMessageInterceptor(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new()
    )
    {
        var dbContext = eventData.Context;
        if (dbContext is not null)
            SaveDomainEvents(dbContext);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void SaveDomainEvents(DbContext dbContext)
    {
        var messages = dbContext
            .ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .SelectMany(e =>
            {
                var events = e.DomainEvents.ToList();
                e.ClearDomainEvents();
                return events;
            })
            .Select(@event => new OutboxMessage
            {
                EventId = Guid.NewGuid(),
                Type = @event.GetType().FullName ?? throw new Exception($"Unable to resolve the fullname of {@event}"),
                Content = JsonSerializer.Serialize(@event),
                OccurredOn = DateTimeOffset.Now
            });

        _dbContext
            .Set<OutboxMessage>()
            .AddRange(messages);
    }
}
