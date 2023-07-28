using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Contract
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDTO>> GetUsersAsync();

        Task<ReadUserDTO> GetUserByIdAsync(int id);

        Task<ReadUserDTO> GetUserByEmailAsync(string email);

        Task<ReadUserDTO> AddUserAsync(CreateUserDTO userDTO);

        Task<ReadUserDTO> RemoveUserAsync(int id);
    }
}