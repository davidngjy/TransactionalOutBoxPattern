using MediatR;
using Microsoft.Extensions.Logging;
using TransactionalOutBoxPattern.Application.IntegrationEvents;
using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices;

internal class KafkaIntegrationEventHandler :
    IIntegrationEventHandler<MessageIntegrationEvent<EmployeeAdded>>
{
    private readonly ILogger<KafkaIntegrationEventHandler> _logger;

    public KafkaIntegrationEventHandler(ILogger<KafkaIntegrationEventHandler> logger)
        => _logger = logger;

    public Task<Unit> Handle(MessageIntegrationEvent<EmployeeAdded> message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Producing Kafka Message with" +
                               "Event Id: {EventId}," +
                               "Occurred On: {OccurredOn}," +
                               "Employee Id: {EmployeeId}," +
                               "Department Id: {DepartmentId}," +
                               "Salary: {Salary}",
            message.EventId,
            message.OccurredOn,
            message.Content.EmployeeId,
            message.Content.DepartmentId.Id,
            message.Content.Salary.Amount);

        return Unit.Task;
    }
}
