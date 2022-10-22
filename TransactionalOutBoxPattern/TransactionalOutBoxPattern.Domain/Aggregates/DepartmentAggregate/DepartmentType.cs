namespace TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

public class DepartmentType : Enumeration<DepartmentType, int>
{
    public DepartmentType HumanResource = new(nameof(HumanResource), 1);
    public DepartmentType Technology = new(nameof(Technology), 2);

    public DepartmentType(string name, int value) : base(name, value) { }
}
