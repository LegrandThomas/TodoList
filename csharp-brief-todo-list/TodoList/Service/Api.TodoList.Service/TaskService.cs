using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;

namespace Api.TodoList.Service
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Data.Entity.Model.Task> _taskRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Status> _statusRepository;

        public TaskService(IRepository<Data.Entity.Model.Task> taskRepository, IRepository<User> userRepository, IRepository<Status> statusRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;
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

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetById(userId, u => u.Tasks).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User {userId} not found.");
            }

            return user.Tasks.Select(TaskMapper.TransformEntityToReadDTO);
        }

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksByStatusIdAsync(int statusId)
        {
            var status = await _statusRepository.GetById(statusId).ConfigureAwait(false);
            if (status == null)
            {
                throw new Exception($"Status {status} not found.");
            }

            var tasks = await _taskRepository.GetAll(t => t.User).ConfigureAwait(false);
            return tasks.Where(t => t.IdStatus == statusId).Select(TaskMapper.TransformEntityToReadDTO);
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