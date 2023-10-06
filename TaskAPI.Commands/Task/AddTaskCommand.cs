using MediatR;
using TaskAPI.Common.DTO;

namespace TaskAPI.Commands.Task
{
    public class AddTaskCommand : IRequest<TaskDTO>
    {
        public TaskDTO Task { get; set; }
    }
}