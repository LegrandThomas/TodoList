using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;
using AutoMapper;
using Task = Api.TodoList.Data.Entity.Model.Task;

namespace Api.TodoList.Service.Mapper
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<Task, ReadTaskDTO>()
                .ForMember(dest => dest.IdStatus, opt => opt.MapFrom(src => src.IdStatus))
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                .ReverseMap();


            CreateMap<Task, CreateTaskDTO>()
               .ReverseMap();

        
        }
    }
}