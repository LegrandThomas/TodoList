using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;
using AutoMapper;
using Task = Api.TodoList.Data.Entity.Model.Task;

namespace Api.TodoList.Service.Mapper
{
    public class UserMapper : Profile 
    {
        public UserMapper()
        {
           


            CreateMap<User, CreateUserDTO>()
                  .ReverseMap();

            CreateMap<User, ReadUserDTO>()
                .ForMember(dest=>dest.Tasks, opt=>opt.MapFrom(src=>src.Tasks))
               .IncludeBase<User, CreateUserDTO>()
                 .ReverseMap();
        }
    }
}