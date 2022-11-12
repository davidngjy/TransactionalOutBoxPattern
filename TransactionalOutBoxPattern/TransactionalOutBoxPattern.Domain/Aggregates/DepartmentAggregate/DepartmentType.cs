namespace TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

public class DepartmentType : Enumeration<DepartmentType>
{
    public static DepartmentType HumanResource = new(nameof(HumanResource), 1);
    public static DepartmentType Technology = new(nameof(Technology), 2);

    public DepartmentType(string name, int value) : base(name, value)
    {
    }
}
