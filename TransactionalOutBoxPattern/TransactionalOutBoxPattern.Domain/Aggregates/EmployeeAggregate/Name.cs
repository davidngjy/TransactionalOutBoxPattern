namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public record Name(string FirstName, string LastName)
{
    public string FullName => FirstName + LastName;
}
