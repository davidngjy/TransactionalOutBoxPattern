using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Application.IntegrationEvents;
using TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices.Models;
using TransactionalOutBoxPattern.Infrastructure.Persistence;

namespace TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices;

internal class IntegrationEventService : IIntegrationEventService
{
    private readonly ISender _sender;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<IntegrationEventService> _logger;

    public IntegrationEventService(ISender sender, ApplicationDbContext dbContext, ILogger<IntegrationEventService> logger)
    {
        _sender = sender;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ProcessIntegrationEvents(CancellationToken cancellationToken)
    {
        var outboxMessages = await _dbContext
            .Set<OutboxMessage>()
            .Where(x => x.ProcessedOn == null)
            .OrderByDescending(x => x.OccurredOn)
            .Take(10)
            .ToListAsync(cancellationToken);

        foreach (var message in outboxMessages)
        {
            try
            {
                var eventType = Type.GetType(message.Type, true)!;
                var @event = JsonSerializer.Deserialize(message.Content, eventType)!;
                await _sender.Send(@event, cancellationToken);

                message.ProcessedOn = DateTimeOffset.UtcNow;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process Event ID: {EventId}", message.EventId);
                message.Error = ex.ToString();

                await _dbContext.SaveChangesAsync(cancellationToken);

                throw new Exception("Exception occurred while processing integration events");
            }
        }
    }

    public void AddIntegrationEvent(IIntegrationEvent message)
    {
        var outboxMessage = new OutboxMessage
        {
            EventId = Guid.NewGuid(),
            Type = message.TypeName,
            Content = message.SerializedContent,
            OccurredOn = DateTimeOffset.UtcNow
        };

        _dbContext
            .Set<OutboxMessage>()
            .Add(outboxMessage);
    }
}
