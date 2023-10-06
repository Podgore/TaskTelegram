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
    public class AddGetDescriptionHandler : IDialogueHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IMediator _mediator;

        public UserState State => UserState.AddGetDescription;

        public AddGetDescriptionHandler(ITelegramBotClient botClient, IMediator mediator)
        {
            _botClient = botClient;
            _mediator = mediator;
        }


        public async Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken)
        {

            context.AddContext.Description = update.Message.Text;
            var taskDto = new TaskDTO { Name = context.AddContext.Title, Description = context.AddContext.Description };
            var result = await _mediator.Send(new AddTaskCommand() { Task = taskDto });
            await _botClient.SendTextMessageAsync(context.ChatId, "Task was added successfully", cancellationToken: cancellationToken);
            context.UserState = UserState.InitialState;
        }
    }
}
