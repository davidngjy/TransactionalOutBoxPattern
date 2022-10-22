namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public sealed class Task : Entity<int>
{
    public string Name { get; }

    public bool IsCompleted { get; private set; }

    public Task(string name) : base(default)
    {
        Name = name;
    }

    public void MarkAsCompleted() => IsCompleted = true;
}
