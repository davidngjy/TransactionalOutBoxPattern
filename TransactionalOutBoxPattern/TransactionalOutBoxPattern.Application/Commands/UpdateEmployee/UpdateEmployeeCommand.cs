using System.Text.Json.Serialization;

namespace TransactionalOutBoxPattern.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : ICommand
{
    [JsonIgnore]
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public decimal Salary { get; set; }
}
