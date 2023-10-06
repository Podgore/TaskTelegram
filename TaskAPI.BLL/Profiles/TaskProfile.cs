using AutoMapper;
using TaskAPI.Common.DTO;
using TaskAPI.Entities;

namespace TaskAPI.BLL.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entities.Task, TaskDTO>().ReverseMap();
            CreateMap<Entities.Task, UpdateTaskDTO>().ReverseMap();
        }
    }
}
