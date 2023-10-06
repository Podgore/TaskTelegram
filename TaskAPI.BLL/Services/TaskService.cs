using AutoMapper;
using TaskAPI.Abstractions.EF;
using TaskAPI.Abstractions.Services;
using TaskAPI.Common.DTO;
using Task = TaskAPI.Entities.Task;

namespace TaskAPI.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepo<Task, string> _taskRepository;

        private readonly IMapper _mapper;

        public TaskService(IRepo<Task, string> taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDTO> AddTask(TaskDTO task)
        {
            Task entity = _mapper.Map<Task>(task);
            if (await _taskRepository.Table.FindAsync(entity.Id) != null)
                throw new InvalidOperationException("Entity with such key already exists in database");
            await _taskRepository.AddAsync(entity);
            return _mapper.Map<TaskDTO>(entity);
        }

        public async Task<bool> DeleteTask(string Name)
        {
            var task = await _taskRepository.FindAsync(Name);
            return task != null && await _taskRepository.DeleteAsync(task) > 0;
        }

        public List<TaskDTO> GetTask()
        {
            return _mapper.Map<IEnumerable<TaskDTO>>(_taskRepository.GetAll()).ToList();
        }

        public async Task<TaskDTO?> GetTaskByName(string Name)
        {
            Task? task = await _taskRepository.FindAsync(Name);
            return task != null ? _mapper.Map<TaskDTO>(Name) : null;
        }

        public async Task<TaskDTO> UpdateDate(string Name, UpdateTaskDTO task)
        {
            var entity = await _taskRepository.FindAsync(Name);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Unable to find entity with such key {Name}");
            }

            _mapper.Map(Name, entity);

            await _taskRepository.UpdateAsync(entity);

            return _mapper.Map<TaskDTO>(Name);
        }
    }
}
