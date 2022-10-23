using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;
using TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Configuration;

internal class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("employee");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .OwnsOne(
                x => x.DepartmentId,
                d =>
                {
                    d.Property(x => x.Id)
                        .HasColumnName("department_id");

                    d.HasOne<Department>()
                        .WithMany()
                        .HasForeignKey(x => x.Id);

                    d.WithOwner();
                });

        builder
            .OwnsOne(
                x => x.Salary,
                s =>
                {
                    s.Property(x => x.Amount)
                        .HasColumnName("salary_amount");

                    s.WithOwner();
                });

        builder
            .HasMany(x => x.Tasks)
            .WithOne()
            .HasForeignKey("EmployeeId");
    }
}
