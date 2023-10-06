using MediatR;
using TaskAPI.Common.DTO;

namespace TaskAPI.Commands.Task
{
    public class GetTaskByIdQuery : IRequest<TaskDTO?>
    {
        public string TaskName { get; }

        public GetTaskByIdQuery(string taskName)
        {
            TaskName = taskName;
        }
    }
}