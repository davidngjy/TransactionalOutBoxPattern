using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionalOutBoxPattern.Domain;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;

internal class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DomainEventInterceptor(IPublisher publisher)
        => _publisher = publisher;

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;
        if (dbContext is not null)
            await PublishDomainEvents(dbContext, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext dbContext, CancellationToken token)
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
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, token);
        }
    }
}
