using MediatR;
using TaskAPI.Abstractions.Dialogues;
using TaskAPI.Commands.Task;
using TaskAPI.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskAPI.Application.Dialogues.Handlers
{
    public class DeleteGetTitleHandler : IDialogueHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IMediator _mediator;
        public UserState State => UserState.DeleteGetTitle;


        public DeleteGetTitleHandler(ITelegramBotClient botClient, IMediator mediator)
        {
            _botClient = botClient;
            _mediator = mediator;
        }


        public async Task ProcessAsync(UserContext context, Update update, CancellationToken cancellationToken)
        {

            context.DeleteContext = new DeleteContext();
            context.DeleteContext.Title = update.Message.Text;
            var result = await _mediator.Send(new DeleteTaskCommand(context.DeleteContext.Title));
            await _botClient.SendTextMessageAsync(context.ChatId, "Task was deleted successfuly", cancellationToken: cancellationToken);
            context.UserState = UserState.InitialState;

        }
    }

}
