using TaskAPI.Common.Enums;
using Telegram.Bot.Types;

namespace TaskAPI.Abstractions.Dialogues
{
    public interface IDialogueHandler
    {
        UserState State { get; }

        Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken);
    }
}
