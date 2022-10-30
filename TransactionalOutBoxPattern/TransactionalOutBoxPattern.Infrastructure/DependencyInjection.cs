using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.Repositories;
using TransactionalOutBoxPattern.Infrastructure.IntegrationEventServices;
using TransactionalOutBoxPattern.Infrastructure.Persistence;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Interceptors;
using TransactionalOutBoxPattern.Infrastructure.Persistence.Repositories;

namespace TransactionalOutBoxPattern.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddScoped<AuditInterceptor>()
            //.AddScoped<OutboxMessageInterceptor>()
            .AddScoped<DomainEventInterceptor>()
            .AddTransient<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddTransient<IDatabaseMigration>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddTransient<IDepartmentRepository, DepartmentRepository>()
            .AddTransient<IEmployeeRepository, EmployeeRepository>()
            .AddTransient<IIntegrationEventService, IntegrationEventService>()
            .AddMediatR(typeof(DependencyInjection));

        services
            .AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var auditInterceptor = provider.GetRequiredService<AuditInterceptor>();
                //var outboxMessageInterceptor = provider.GetRequiredService<OutboxMessageInterceptor>();
                var domainEventInterceptor = provider.GetRequiredService<DomainEventInterceptor>();
                var configuration = provider.GetRequiredService<IConfiguration>();

                builder
                    .UseNpgsql(configuration.GetConnectionString("Postgresql"))
                    //.AddInterceptors(auditInterceptor, outboxMessageInterceptor);
                    .AddInterceptors(auditInterceptor, domainEventInterceptor);
            });

        return services;
    }
}
