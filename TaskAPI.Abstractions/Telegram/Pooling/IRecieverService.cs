namespace TaskAPI.Abstractions.Telegram.Pooling;

public interface IRecieverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}