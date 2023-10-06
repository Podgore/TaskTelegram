using MediatR;
using TaskAPI.Abstractions.Services;
using TaskAPI.Commands.Task;
using TaskAPI.Common.DTO;

namespace TaskAPI.Handlers.Task;

public class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByIdQuery, TaskDTO?>
{
    private readonly ITaskService _taskService;

    public GetTaskByIdQueryHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskDTO?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _taskService.GetTaskByName(request.TaskName);
    }
}
