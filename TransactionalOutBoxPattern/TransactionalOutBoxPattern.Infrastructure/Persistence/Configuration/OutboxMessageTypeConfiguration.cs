using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices.Models;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Configuration;

internal class OutboxMessageTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder
            .ToTable("outbox_message");

        builder
            .HasKey(x => x.EventId);

        builder
            .Property(x => x.EventId)
            .HasColumnName("event_id");

        builder
            .Property(x => x.Type)
            .HasColumnName("type");

        builder
            .Property(x => x.Content)
            .HasColumnName("content");

        builder
            .Property(x => x.OccurredOn)
            .HasColumnName("occurred_on");

        builder
            .Property(x => x.ProcessedOn)
            .HasColumnName("processed_on");

        builder
            .Property(x => x.Error)
            .HasColumnName("error");
    }
}
