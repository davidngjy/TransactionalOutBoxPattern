﻿namespace TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

public class Department : AuditableEntity<Guid>, IAggregateRoot
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

    public void AddTotalSalary(decimal amount)
        => DepartmentTotalSalary += amount;
}
