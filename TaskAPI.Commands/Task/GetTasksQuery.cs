using MediatR;
using TaskAPI.Common.DTO;

namespace TaskAPI.Commands.Task
{
    public record GetTasksQuery : IRequest<List<TaskDTO>>;
}