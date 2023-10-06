using TaskAPI.Abstractions.Dialogues;
using TaskAPI.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskAPI.Application.Dialogues.Handlers
{
    public class InitialDialogueHandler : IDialogueHandler
    {
        private readonly ITelegramBotClient _botClient;

        public UserState State => UserState.InitialState;

        public InitialDialogueHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken)
        {
            switch (update.Message?.Text)
            {
                case "/add":
                    await _botClient.SendTextMessageAsync(context.ChatId, "Enter title for new task:", cancellationToken: cancellationToken);
                    context.UserState = UserState.AddGetTitle;
                    
                    break;
                case "/delete":
                    await _botClient.SendTextMessageAsync(context.ChatId, "Enter title for deleted task:", cancellationToken: cancellationToken);
                    context.UserState = UserState.DeleteGetTitle;
                    break;
                case "/get":
                    await _botClient.SendTextMessageAsync(context.ChatId, "If you want to get all, write 'all', if no, write title of geter task", cancellationToken: cancellationToken);
                    context.UserState = UserState.GetAllTitle;
                    break;
                default:
                    throw new Exception("Unable to process your input");
            }
            
        }
    }


}
