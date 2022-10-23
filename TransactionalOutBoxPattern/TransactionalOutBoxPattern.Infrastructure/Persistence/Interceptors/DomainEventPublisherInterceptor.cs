using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionalOutBoxPattern.Domain;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;

internal class DomainEventPublisherInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DomainEventPublisherInterceptor(IPublisher publisher) => _publisher = publisher;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new()
    )
    {
        var dbContext = eventData.Context;
        if (dbContext is not null)
            await PublishDomainEvents(dbContext, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext dbContext, CancellationToken cancellationToken)
    {
        var entities = dbContext
            .ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .ToList();

        var events = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities
            .ForEach(e => e.ClearDomainEvents());

        foreach (var @event in events)
        {
            await _publisher.Publish(@event, cancellationToken);
        }
    }
}
