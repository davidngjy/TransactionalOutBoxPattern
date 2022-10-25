using System.Text.Json.Serialization;
using TransactionalOutBoxPattern.Application.Abstraction;

namespace TransactionalOutBoxPattern.Application.Commands.CreateEmployee;

public record CreateEmployeeCommand : ICommand<Guid>
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Role { get; set; } = "";

    [JsonIgnore]
    public Guid DepartmentId { get; set; }

    public decimal SalaryAmount { get; set; }
}
