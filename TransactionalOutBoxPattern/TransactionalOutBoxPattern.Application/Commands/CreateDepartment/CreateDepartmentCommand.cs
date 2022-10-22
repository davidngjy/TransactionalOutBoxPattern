using TransactionalOutBoxPattern.Application.Abstraction;

namespace TransactionalOutBoxPattern.Application.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name, string DepartmentType)
    : ICommand<Guid>, ICommand;
