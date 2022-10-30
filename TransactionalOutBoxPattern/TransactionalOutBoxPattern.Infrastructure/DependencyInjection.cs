﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Infrastructure.Persistence;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Outbox;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Repositories;

namespace TransactionalOutBoxPattern.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddScoped<AuditInterceptor>()
            .AddScoped<OutboxMessageInterceptor>()
            .AddTransient<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddTransient<IDatabaseMigration>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddTransient<IDepartmentRepository, DepartmentRepository>()
            .AddTransient<IEmployeeRepository, EmployeeRepository>()
            .AddTransient<IOutboxMessageHandler, OutboxMessageHandler>();

        services
            .AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var auditInterceptor = provider.GetRequiredService<AuditInterceptor>();
                var outboxMessageInterceptor = provider.GetRequiredService<OutboxMessageInterceptor>();
                var configuration = provider.GetRequiredService<IConfiguration>();

                builder
                    .UseNpgsql(configuration.GetConnectionString("Postgresql"))
                    .AddInterceptors(auditInterceptor, outboxMessageInterceptor);
            });

        return services;
    }
}
