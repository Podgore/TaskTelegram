using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Abstractions.Dialogues;
using TaskAPI.Commands.Task;
using TaskAPI.Common.DTO;
using TaskAPI.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskAPI.Application.Dialogues.Handlers
{
    public class GetAllTitle
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IMediator _mediator;

        public UserState State => UserState.GetAllTitle;

        public GetAllTitle(ITelegramBotClient botClient, IMediator mediator)
        {
            _botClient = botClient;
            _mediator = mediator;
        }


        public async Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken)
        {
            var choice = update.Message.Text;
            if (choice == "all")
                await _mediator.Send(new GetTasksQuery());
            else
            {
                context.GetContext.Title = update.Message.Text;
                await _mediator.Send(new GetTaskByIdQuery(context.GetContext.Title));
            }

            await _botClient.SendTextMessageAsync(context.ChatId, "Get task", cancellationToken: cancellationToken);
            context.UserState = UserState.InitialState;
        }
    }
}
