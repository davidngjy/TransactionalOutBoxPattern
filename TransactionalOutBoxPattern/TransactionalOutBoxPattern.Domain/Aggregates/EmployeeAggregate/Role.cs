namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public class Role : Enumeration<Role, int>
{
    public static Role Manager = new(nameof(Manager), 1);
    public static Role Developer = new(nameof(Developer), 1);

    private Role(string name, int value)
        : base(name, value) { }
}
