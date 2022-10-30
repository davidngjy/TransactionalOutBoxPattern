using TransactionalOutBoxPattern.Application.IntegrationEvents;

namespace TransactionalOutBoxPattern.Application.Abstraction;

public interface IIntegrationEventService
{
    Task ProcessIntegrationEvents(CancellationToken cancellationToken);

    void AddIntegrationEvent(IIntegrationEvent message);
}
