using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;

namespace Api.TodoList.Service
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Data.Entity.Model.Task> _taskRepository;

        public TaskService(IRepository<Data.Entity.Model.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetAll(t => t.User).ConfigureAwait(false);
            return tasks.Select(TaskMapper.TransformEntityToReadDTO);
        }

        public async Task<ReadTaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetById(id, t => t.User).ConfigureAwait(false);
            if (task == null)
            {
                throw new Exception($"Task {id} not found.");
            }

            return TaskMapper.TransformEntityToReadDTO(task);
        }

        public async Task<ReadTaskDTO> AddTaskAsync(CreateTaskDTO createTaskDTO)
        {
            if (createTaskDTO == null)
            {
                throw new ArgumentNullException(nameof(createTaskDTO));
            }

            var taskToAdd = TaskMapper.TransformCreateDTOToEntity(createTaskDTO);

            var taskAdded = await _taskRepository.Add(taskToAdd).ConfigureAwait(false);
            return TaskMapper.TransformEntityToReadDTO(taskAdded);
        }

        public async Task<ReadTaskDTO> RemoveTaskAsync(int id)
        {
            var task = await _taskRepository.GetById(id).ConfigureAwait(false);
            if (task == null)
            {
                throw new Exception($"Task {id} not found.");
            }

            var taskDeleted = await _taskRepository.Remove(task).ConfigureAwait(false);
            return TaskMapper.TransformEntityToReadDTO(taskDeleted);
        }
    }
}