namespace Api.TodoList.Service.Contract
{
    public interface ITaskService
    {
        Task<IEnumerable<Data.Entity.Model.Task>> GetTasksAsync();
    }
}