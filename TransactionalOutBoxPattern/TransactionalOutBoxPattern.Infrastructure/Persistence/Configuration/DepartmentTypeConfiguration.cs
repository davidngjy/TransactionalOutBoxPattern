using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionalOutBoxPattern.Domain.Aggregates.DepartmentAggregate;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Configuration;

internal class DepartmentTypeConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .ToTable("department");

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
            .Property(x => x.DepartmentType)
            .HasColumnName("department_type")
            .HasConversion(
                x => x.Name,
                x => DepartmentType.FromName(x)
            )
            .IsRequired();

        builder
            .Property(x => x.DepartmentTotalSalary)
            .HasColumnName("total_salary")
            .IsRequired();

        builder
            .Property(x => x.CreatedOn)
            .HasColumnName("created_on")
            .IsRequired();

        builder
            .Property(x => x.ModifiedOn)
            .HasColumnName("modified_on");
    }
}
