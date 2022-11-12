namespace TransactionalOutBoxPattern.Application.Models;

public record Employee
{
    public required string Name { get; init; }
    public required string Role { get; init; }
    public required decimal Salary { get; init; }
}
