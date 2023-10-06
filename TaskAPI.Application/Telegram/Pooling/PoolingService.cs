using Microsoft.Extensions.Logging;
using TaskAPI.Abstractions.Telegram.Pooling;

namespace TaskAPI.Application.Telegram.Pooling;

public class PoolingService : BasePollingService<ReceiverService>
{
    public PoolingService(IServiceProvider serviceProvider, ILogger<PoolingService> logger)
        : base(serviceProvider, logger) { }
}