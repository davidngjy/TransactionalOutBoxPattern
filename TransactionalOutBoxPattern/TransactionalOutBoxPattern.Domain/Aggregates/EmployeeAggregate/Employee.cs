using TransactionalOutBoxPattern.Domain.Exceptions;

namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public sealed class Employee : Entity<Guid>, IAggregateRoot
{
    public Name Name { get; }

    public Role Role { get; }

    public DepartmentId DepartmentId { get; }

    public Salary Salary { get; private set; }

    private readonly List<Task> _tasks = new();

    public IReadOnlyList<Task> Tasks => _tasks.AsReadOnly();

    public Employee(Guid id, Name name, Role role, DepartmentId departmentId, Salary salary)
        : base(id)
    {
        Name = name;
        Role = role;
        DepartmentId = departmentId;
        Salary = salary;
    }

    public void UpdateSalary(decimal amount)
        => Salary = Salary with { Amount = amount };

    public void AddTask(string name)
        => _tasks.Add(new Task(name));

    public void MarkTaskAsDone(string name)
    {
        var task = _tasks
            .Where(x => x.Name == name)
            .FirstOrDefault();

        if (task is null)
            throw new TaskNotFoundException($"Unable to find task {name}");

        task.MarkAsCompleted();
    }
}
