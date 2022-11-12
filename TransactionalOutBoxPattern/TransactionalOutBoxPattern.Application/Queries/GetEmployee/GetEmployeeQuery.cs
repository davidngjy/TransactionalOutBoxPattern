using TransactionalOutBoxPattern.Application.Models;

namespace TransactionalOutBoxPattern.Application.Queries.GetEmployee;

public record GetEmployeeQuery(Guid EmployeeId) : IQuery<Employee>;
