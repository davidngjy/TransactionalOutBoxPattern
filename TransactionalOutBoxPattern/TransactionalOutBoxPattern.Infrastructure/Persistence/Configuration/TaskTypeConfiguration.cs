using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TransactionalOutBoxPattern.Domain.Aggregates.EmployeeAggregate.Task;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Configuration;

internal class TaskTypeConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder
            .ToTable("task");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder
            .Property(x => x.Name)
            .HasColumnName("name");

        builder
            .Property(x => x.IsCompleted)
            .HasColumnName("is_completed");

        builder
            .Property<Guid>("EmployeeId")
            .HasColumnName("employee_id");
    }
}
