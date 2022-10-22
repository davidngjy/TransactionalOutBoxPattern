namespace TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

public class Department : Entity<Guid>, IAggregateRoot
{
    public string Name { get; }

    public DepartmentType DepartmentType { get; }

    public decimal DepartmentTotalSalary { get; private set; }

    public Department(Guid id, string name, DepartmentType departmentType)
        : base(id)
    {
        Name = name;
        DepartmentType = departmentType;
        DepartmentTotalSalary = 0;
    }

    public void IncreaseTotalSalary(decimal amount)
        => DepartmentTotalSalary += amount;

    public void DecreaseTotalSalary(decimal amount)
        => DepartmentTotalSalary -= amount;
}
