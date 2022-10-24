using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalOutBoxPattern.Application.Abstraction;
using TransactionalOutBoxPattern.Domain.Repositories;
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
            .AddScoped<DomainEventPublisherInterceptor>()
            .AddTransient<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddTransient<IDepartmentRepository, DepartmentRepository>()
            .AddTransient<IEmployeeRepository, EmployeeRepository>();

        services
            .AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var auditInterceptor = provider.GetRequiredService<AuditInterceptor>();
                var domainEventPublisherInterceptor = provider.GetRequiredService<DomainEventPublisherInterceptor>();
                var configuration = provider.GetRequiredService<IConfiguration>();

                builder
                    .UseNpgsql(configuration.GetConnectionString("Postgresql"))
                    .AddInterceptors(auditInterceptor, domainEventPublisherInterceptor);
            });

        return services;
    }
}
