using TaskAPI.Common.DTO;

namespace TaskAPI.Abstractions.Services
{
    public interface ITaskService
    {
        Task<bool> DeleteTask(string Name);
        Task<TaskDTO> AddTask(TaskDTO task);
        List<TaskDTO> GetTask();
        Task<TaskDTO?> GetTaskByName(string Name);
        Task<TaskDTO> UpdateDate(string Name, UpdateTaskDTO task);
    }
}
