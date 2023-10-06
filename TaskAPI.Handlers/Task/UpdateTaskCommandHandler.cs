using MediatR;
using TaskAPI.Abstractions.Services;
using TaskAPI.Commands.Task;
using TaskAPI.Common.DTO;

namespace TaskAPI.Handlers.Task;

public class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, TaskDTO>
{
    private readonly ITaskService _taskService;

    public UpdateTaskCommandHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskDTO> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        return await _taskService.UpdateDate(request.TaskName, request.UpdatedTask);
    }
}
