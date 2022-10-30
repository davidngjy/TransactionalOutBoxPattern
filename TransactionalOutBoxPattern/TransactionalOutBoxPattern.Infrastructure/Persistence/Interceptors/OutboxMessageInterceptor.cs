using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionalOutBoxPattern.Domain;
using TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices.Models;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;

internal class OutboxMessageInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;
        if (dbContext is not null)
            SaveDomainEvents(dbContext);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SaveDomainEvents(DbContext dbContext)
    {
        var domainEvents = dbContext
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
                Type = @event.TypeName,
                Content = @event.SerializedContent,
                OccurredOn = DateTimeOffset.UtcNow
            })
            .ToList();

        dbContext
            .Set<OutboxMessage>()
            .AddRange(domainEvents);
    }
}
