using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Mapper
{
    public class UserMapper
    {
        public static User TransformCreateDTOToEntity(CreateUserDTO userDto)
        {
            return new User
            {
                LastName = userDto.LastName,
                FirstName = userDto.FirstName,
                Email = userDto.Email,
                Tasks = userDto.Tasks.Select(TaskMapper.TransformCreateDTOToEntity).ToList()
            };
        }

        public static ReadUserDTO TransformEntityToReadDTO(User user)
        {
            return new ReadUserDTO
            {
                IdUser = user.IdUser,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Tasks = user.Tasks.Select(TaskMapper.TransformEntityToReadDTO).ToList()
            };
        }
    }
}