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
            .Property(x => x.Role)
            .HasConversion(
                x => x.Name,
                x => Role.FromName(x))
            .HasColumnName("role");

        builder
            .OwnsOne(
                x => x.Name,
                n =>
                {
                    n.Property(x => x.FirstName)
                        .HasColumnName("first_name");

                    n.Property(x => x.LastName)
                        .HasColumnName("last_name");

                    n.WithOwner();
                });

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

        builder
            .Property(x => x.CreatedOn)
            .HasColumnName("created_on")
            .IsRequired();

        builder
            .Property(x => x.ModifiedOn)
            .HasColumnName("modified_on");

        builder
            .Navigation(x => x.Tasks)
            .AutoInclude();
    }
}
