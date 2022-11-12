using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Application.ApplicationResult;
using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Commands.UpdateEmployee;

internal class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        if (employee is null)
            return NotFoundResult.Create();

        employee.UpdateName(new Name(command.FirstName, command.LastName));
        employee.UpdateSalary(new Salary(command.Salary));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Successful;
    }
}
