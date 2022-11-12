namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public abstract class Role : Enumeration<Role>
{
    public static Role Manager = new ManagerRole();
    public static Role Developer = new DeveloperRole();

    private Role(string name, int value)
        : base(name, value)
    {
    }

    public abstract decimal BonusPercentage { get; }

    private sealed class ManagerRole : Role
    {
        public ManagerRole() : base("Manager", 1)
        {
        }

        public override decimal BonusPercentage => 0.1m;
    }

    private sealed class DeveloperRole : Role
    {
        public DeveloperRole() : base("Developer", 2)
        {
        }

        public override decimal BonusPercentage => 0.2m;
    }
}
