using MediatR;
using TaskAPI.Abstractions.Services;
using TaskAPI.Commands.Task;

namespace TaskAPI.Handlers.Task;
public class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskService _taskService;

    public DeleteTaskCommandHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTask(request.TaskName);
    }
}