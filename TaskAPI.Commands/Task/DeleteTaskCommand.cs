using MediatR;

namespace TaskAPI.Commands.Task
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public string TaskName { get; set; }

        public DeleteTaskCommand(string Name)
        {
            TaskName = Name;
        }
    }
}