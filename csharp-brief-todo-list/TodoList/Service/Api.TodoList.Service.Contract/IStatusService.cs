using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Contract
{
    public interface IStatusService
    {
        Task<IEnumerable<ReadStatusDTO>> GetStatusesAsync();

        Task<ReadStatusDTO> GetStatusByIdAsync(int idStatus);
    }
}