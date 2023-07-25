using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Contract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<ReadUserDTO> GetUserByIdAsync(int id);
    }
}