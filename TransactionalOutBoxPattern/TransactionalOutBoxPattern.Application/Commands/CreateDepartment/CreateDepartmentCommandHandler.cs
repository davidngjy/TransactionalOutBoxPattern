using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Domain.Results;

namespace TransactionalOutBoxPattern.Application.Commands.CreateDepartment;

internal class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
    {
        _unitOfWork = unitOfWork;
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var newDepartmentId = Guid.NewGuid();
        var newDepartment = new Department(
            newDepartmentId,
            command.Name,
            DepartmentType.FromName(command.DepartmentType)
        );

        _departmentRepository.Add(newDepartment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Successful(newDepartmentId);
    }
}
