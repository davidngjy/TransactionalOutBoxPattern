using MediatR;
using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

namespace TransactionalOutBoxPattern.Domain.DomainEvents;

public record EmployeeAdded(Guid EmployeeId, Salary Salary, DepartmentId DepartmentId)
    : INotification;