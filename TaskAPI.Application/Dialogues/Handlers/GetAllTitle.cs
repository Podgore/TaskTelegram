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
    public class GetAllTitle : IDialogueHandler
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
            context.GetContext = new GetContext();
            var choice = update.Message.Text;
            if (choice == "all")
            {
                var result = await _mediator.Send(new GetTasksQuery());
                await _botClient.SendTextMessageAsync(context.ChatId, "Geting tasks:", cancellationToken: cancellationToken);
                foreach (var task in result)
                {
                    await _botClient.SendTextMessageAsync(context.ChatId, $"Name: {task.Name} \nDescription: {task.Description}", cancellationToken: cancellationToken);
                    
                }
            }
            else
            {
                context.GetContext.Title = update.Message.Text;
                var task = await _mediator.Send(new GetTaskByIdQuery(context.GetContext.Title));
                await _botClient.SendTextMessageAsync(context.ChatId, $"Name: {task.Name} \nDescription: {task.Description}", cancellationToken: cancellationToken);
            }

            
            context.UserState = UserState.InitialState;
        }
    }
}
