using MediatR;
using TransactionalOutBoxPattern.Application.IntegrationEvents;

namespace TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices;

public interface IIntegrationEventHandler<in T> : IRequestHandler<T>
    where T : IIntegrationEvent
{
}
