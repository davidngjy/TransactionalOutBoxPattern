using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalOutBoxPattern.Infrastructure.Persistence;

namespace TransactionalOutBoxPattern.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                builder.UseNpgsql(configuration.GetConnectionString("Postgresql"));
            });

        return services;
    }
}
