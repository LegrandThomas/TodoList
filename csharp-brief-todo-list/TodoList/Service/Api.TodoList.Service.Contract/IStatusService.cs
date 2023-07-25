using Api.TodoList.Data.Entity.Model;

namespace Api.TodoList.Service.Contract
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetStatusesAsync();
    }
}