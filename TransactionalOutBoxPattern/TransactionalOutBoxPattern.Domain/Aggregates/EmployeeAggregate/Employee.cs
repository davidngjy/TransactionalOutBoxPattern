using TransactionalOutBoxPattern.Domain.DomainEvents;
using TransactionalOutBoxPattern.Domain.Exceptions;

namespace TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

public sealed class Employee : AuditableEntity<Guid>, IAggregateRoot
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

        AddDomainEvent(new EmployeeAdded(id, salary, departmentId));
    }

    private Employee() : base(default)
    {
        Name = default!;
        Role = default!;
        DepartmentId = default!;
        Salary = default!;
    }

    public void UpdateSalary(decimal amount)
        => Salary = Salary with { Amount = amount };

    public void AddTask(string name)
        => _tasks.Add(new Task(name));

    public void MarkTaskAsDone(string name)
    {
        var task = _tasks
            .FirstOrDefault(x => x.Name == name);

        if (task is null)
            throw new TaskNotFoundException($"Unable to find task {name}");

        task.MarkAsCompleted();
    }
}
