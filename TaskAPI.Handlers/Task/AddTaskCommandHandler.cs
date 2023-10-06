using MediatR;
using TaskAPI.Abstractions.Services;
using TaskAPI.Commands.Task;
using TaskAPI.Common.DTO;

namespace TaskAPI.Handlers.Task;

public class AddTaskCommandHandler
    : IRequestHandler<AddTaskCommand, TaskDTO>
{
    private readonly ITaskService _taskService;
    public AddTaskCommandHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskDTO> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        return await _taskService.AddTask(request.Task);
    }
}


