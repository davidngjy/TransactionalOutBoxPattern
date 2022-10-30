using MediatR;
using Microsoft.Extensions.Logging;
using TransactionalOutBoxPattern.Application.IntegrationEvents;
using TransactionalOutBoxPattern.Domain.DomainEvents;

namespace TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices;

public class EmailIntegrationEventHandler
    : IIntegrationEventHandler<EmailIntegrationEvent<EmployeeAdded>>
{
    private readonly ILogger<EmailIntegrationEventHandler> _logger;

    public EmailIntegrationEventHandler(ILogger<EmailIntegrationEventHandler> logger) => _logger = logger;

    public Task<Unit> Handle(EmailIntegrationEvent<EmployeeAdded> message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending Email with the following information" +
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
