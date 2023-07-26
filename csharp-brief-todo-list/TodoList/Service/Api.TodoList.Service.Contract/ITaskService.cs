using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Contract
{
    public interface ITaskService
    {
        Task<IEnumerable<ReadTaskDTO>> GetTasksAsync();

        Task<ReadTaskDTO> GetTaskByIdAsync(int id);

        Task<IEnumerable<ReadTaskDTO>> GetTasksByUserIdAsync(int userId);

        Task<IEnumerable<ReadTaskDTO>> GetTasksByStatusIdAsync(int statusId);

        Task<ReadTaskDTO> AddTaskAsync(CreateTaskDTO createDTO);

        Task<ReadTaskDTO> RemoveTaskAsync(int id);
    }
}