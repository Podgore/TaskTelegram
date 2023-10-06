using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TaskAPI.Abstractions.Telegram.Pooling;

public abstract class BasePollingService<TReceiverService> : BackgroundService
     where TReceiverService : IRecieverService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BasePollingService<TReceiverService>> _logger;

    public BasePollingService(
        IServiceProvider serviceProvider,
        ILogger<BasePollingService<TReceiverService>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
        await DoWork(stoppingToken);

    private async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

                await receiver.ReceiveAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
