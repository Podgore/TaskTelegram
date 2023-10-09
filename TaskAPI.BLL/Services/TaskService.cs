using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Abstractions.EF;
using TaskAPI.Abstractions.Services;
using TaskAPI.Common.DTO;
using Task = TaskAPI.Entities.Task;

namespace TaskAPI.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepo<Task, int> _taskRepository;

        private readonly IMapper _mapper;

        public TaskService(IRepo<Task, int> taskRepository, IMapper mapper)
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
            var entity = await _taskRepository.Table.FirstOrDefaultAsync(t => t.Name == Name);

            var task = await _taskRepository.FindAsync(entity.Id);
            return task != null && await _taskRepository.DeleteAsync(task) > 0;
        }

        public async Task<List<TaskDTO>> GetTask()
        {
            var task = await _taskRepository.GetAll();
            return _mapper.Map<IEnumerable<TaskDTO>>(task).ToList();
        }

        public async Task<TaskDTO?> GetTaskByName(string Name)
        {
            var entity = await _taskRepository.Table.FirstOrDefaultAsync(t => t.Name == Name);
            Task? task = await _taskRepository.FindAsync(entity.Id);
            return task != null ? _mapper.Map<TaskDTO>(task) : null;
        }

        public async Task<TaskDTO> UpdateDate(string Name, UpdateTaskDTO task)
        {
            var entityToUpdate = await _taskRepository.Table.FirstOrDefaultAsync(t => t.Name == Name);
            var entity = await _taskRepository.FindAsync(entityToUpdate.Id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Unable to find entity with such key {Name}");
            }

            _mapper.Map(entity, entity);

            await _taskRepository.UpdateAsync(entity);

            return _mapper.Map<TaskDTO>(entity);
        }
    }
}
