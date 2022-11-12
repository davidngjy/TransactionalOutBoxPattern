using TransactionalOutBoxPattern.Application.ApplicationResult;
using TransactionalOutBoxPattern.Application.Models;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Queries.GetEmployee;

internal class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);

        if (employee is null)
            return NotFoundResult<Employee>.Create();

        return Result<Employee>.Successful(new Employee
        {
            Name = employee.Name.FullName,
            Role = employee.Role.Name,
            Salary = employee.Salary.Amount
        });
    }
}
