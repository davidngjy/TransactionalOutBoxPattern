using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Commands.CreateEmployee;

internal class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var newEmployeeId = Guid.NewGuid();
        var newEmployee = new Employee(
            newEmployeeId,
            new Name(request.FirstName, request.LastName),
            Role.FromName(request.Role),
            new DepartmentId(request.DepartmentId),
            new Salary(request.SalaryAmount)
        );

        _employeeRepository.Add(newEmployee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Successful(newEmployeeId);
    }
}
