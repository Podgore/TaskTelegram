using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Abstractions.Dialogues;
using TaskAPI.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskAPI.Application.Dialogues.Handlers
{
    public class AddGetTitleHandler : IDialogueHandler
    {
        private readonly ITelegramBotClient _botClient;

        public UserState State => UserState.AddGetTitle;

        public AddGetTitleHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        

        public async Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken)
        {
            context.AddContext = new AddContext();
            context.AddContext.Title = update.Message.Text;
            await _botClient.SendTextMessageAsync(context.ChatId, "Enter description for new task:", cancellationToken: cancellationToken);
            context.UserState = UserState.AddGetDescription;
        }
    }
}
