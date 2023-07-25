using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;

namespace Api.TodoList.Service
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Data.Entity.Model.Task> _taskRepository;

        public TaskService(IRepository<Data.Entity.Model.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the tasks entities from our table</returns>
        public async Task<IEnumerable<Data.Entity.Model.Task>> GetTasksAsync()
        {
            return await _taskRepository.GetAll().ConfigureAwait(false);
        }
    }
}