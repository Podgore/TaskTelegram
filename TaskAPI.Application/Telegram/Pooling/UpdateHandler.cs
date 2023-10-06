using MediatR;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TaskAPI.Common.Enums;
using TaskAPI.Abstractions.Dialogues;
using Microsoft.Extensions.Logging;

namespace TaskAPI.Application.Telegram.Pooling;

public class UpdateHandler : IUpdateHandler
{
    private readonly IMediator _mediator;
    private readonly Dictionary<long, UserContext> _userContexts = new();
    private readonly IEnumerable<IDialogueHandler> _handlers;
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(
        IMediator mediator,
        IEnumerable<IDialogueHandler> handlers,
        ILogger<UpdateHandler> logger)
    {
        _mediator = mediator;
        _handlers = handlers;
        _logger = logger;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception.Message);
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message?.Chat.Id ?? throw new NullReferenceException(nameof(update));

        if (!_userContexts.TryGetValue(chatId, out var context))
        {
            context = new UserContext(chatId);
            _userContexts.Add(chatId, context);
        }

        var handler = _handlers.FirstOrDefault(handler => handler.State == context.UserState) ?? throw new InvalidOperationException("Unable to process current user state");

        await handler.ProcessAsync(context, update, cancellationToken);
    }
}