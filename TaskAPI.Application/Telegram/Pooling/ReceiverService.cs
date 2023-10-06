using TaskAPI.Abstractions.Telegram.Pooling;
using Telegram.Bot;

namespace TaskAPI.Application.Telegram.Pooling;

public class ReceiverService : BaseReceiverService<UpdateHandler>
{
    public ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler)
        : base(botClient, updateHandler) { }
}
