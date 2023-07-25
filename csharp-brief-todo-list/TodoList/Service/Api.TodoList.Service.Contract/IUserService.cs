using Api.TodoList.Data.Entity.Model;

namespace Api.TodoList.Service.Contract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);
    }
}