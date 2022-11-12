using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactionalOutBoxPattern.Application.PipelineBehaviors;

namespace TransactionalOutBoxPattern.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(QueryPipelineBehavior<,>));

        return services;
    }
}
