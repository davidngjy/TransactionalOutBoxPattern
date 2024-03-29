﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TransactionalOutBoxPattern.Domain;

namespace TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new()
    )
    {
        var dbContext = eventData.Context;
        if (dbContext is not null)
            UpdateAuditableEntities(dbContext);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext dbContext)
    {
        var entries = dbContext
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                var currentDateTime = DateTimeOffset.UtcNow;
                entry.Property(x => x.CreatedOn).CurrentValue = currentDateTime;
                entry.Property(x => x.ModifiedOn).CurrentValue = currentDateTime;
            }

            if (entry.State is EntityState.Modified || entry.Navigations.Any(x => x.IsModified))
            {
                entry.Property(x => x.ModifiedOn).CurrentValue = DateTimeOffset.UtcNow;
            }
        }
    }
}
