using MediatR;
using TaskAPI.Common.DTO;

namespace TaskAPI.Commands.Task
{
    public class UpdateTaskCommand : IRequest<TaskDTO>
    {
        public string TaskName { get; }
        public UpdateTaskDTO UpdatedTask { get; }
    }
}