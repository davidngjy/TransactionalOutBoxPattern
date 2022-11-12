namespace TransactionalOutBoxPattern.Domain.DomainEvents;

public record EmployeeSalaryUpdated(
    Guid DepartmentId,
    decimal OldSalaryAmount,
    decimal NewSalaryAmount
) : IDomainEvent;
