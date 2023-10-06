using MediatR;
using TaskAPI.Abstractions.Services;
using TaskAPI.Commands.Task;
using TaskAPI.Common.DTO;

namespace TaskAPI.Handlers.Task;

// ToDo: fix smth here
public class GetAllTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDTO>>
{
    private readonly ITaskService _taskService;

    public GetAllTasksQueryHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public List<TaskDTO> Handle(GetTasksQuery request)
    {
        return _taskService.GetTask();
    }

    public Task<List<TaskDTO>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
