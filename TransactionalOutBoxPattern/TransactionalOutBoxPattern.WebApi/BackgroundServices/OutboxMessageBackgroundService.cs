using TransactionalOutBoxPattern.Infrastructure.Persistence.Outbox;

namespace TransactionalOutBoxPattern.WebApi.BackgroundServices;

public class OutboxMessageBackgroundService : BackgroundService
{
    private readonly ILogger<OutboxMessageBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public OutboxMessageBackgroundService(
        ILogger<OutboxMessageBackgroundService> logger,
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
                var outboxMessageHandler = scope.ServiceProvider.GetRequiredService<IOutboxMessageHandler>();
                await outboxMessageHandler.ProcessOutboxMessagesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
