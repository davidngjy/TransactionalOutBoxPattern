using TransactionalOutBoxPattern.Application.Abstraction;

namespace TransactionalOutBoxPattern.WebApi.BackgroundServices;

public class IntegrationEventProcessor : BackgroundService
{
    private readonly ILogger<IntegrationEventProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;

    public IntegrationEventProcessor(
        ILogger<IntegrationEventProcessor> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            try
            {
                var integrationEventService = scope.ServiceProvider.GetRequiredService<IIntegrationEventService>();
                await integrationEventService.ProcessIntegrationEvents(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
