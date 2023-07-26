using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Api.TodoList.Service.Mapper;
using AutoMapper;
using System.Threading.Tasks;
using Task = Api.TodoList.Data.Entity.Model.Task;

namespace Api.TodoList.Service
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Data.Entity.Model.Task> _taskRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Status> _statusRepository;


        private readonly IMapper _mapper;

        public TaskService(IRepository<Data.Entity.Model.Task> taskRepository, IRepository<User> userRepository, IRepository<Status> statusRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;

            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetAll(t => t.User).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ReadTaskDTO>>(tasks);
        }

        public async Task<ReadTaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetById(id, t => t.User).ConfigureAwait(false);
            if (task == null)
            {
                throw new Exception($"Task {id} not found.");
            }

            return _mapper.Map<ReadTaskDTO>(task);
        }

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetById(userId, u => u.Tasks).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"User {userId} not found.");
            }

            return _mapper.Map<IEnumerable<ReadTaskDTO>>(user.Tasks);
        }

        public async Task<IEnumerable<ReadTaskDTO>> GetTasksByStatusIdAsync(int statusId)
        {
            var status = await _statusRepository.GetById(statusId).ConfigureAwait(false);
            if (status == null)
            {
                throw new Exception($"Status {status} not found.");
            }

            var tasks = await _taskRepository.GetAll(t => t.User).ConfigureAwait(false);

            return _mapper.Map<IEnumerable<ReadTaskDTO>>(tasks.Where(t => t.IdStatus == statusId));

        }

        public async Task<ReadTaskDTO> AddTaskAsync(CreateTaskDTO createTaskDTO)
        {
            if (createTaskDTO == null)
            {
                throw new ArgumentNullException(nameof(createTaskDTO));
            }

            var taskToAdd = _mapper.Map<Task>(createTaskDTO);

            var taskAdded = await _taskRepository.Add(taskToAdd).ConfigureAwait(false);

            return _mapper.Map<ReadTaskDTO>(taskAdded);
        }

        public async Task<ReadTaskDTO> RemoveTaskAsync(int id)
        {
            var task = await _taskRepository.GetById(id).ConfigureAwait(false);
            if (task == null)
            {
                throw new Exception($"Task {id} not found.");
            }

            var taskDeleted = await _taskRepository.Remove(task).ConfigureAwait(false);

            return _mapper.Map<ReadTaskDTO>(taskDeleted);
        }
    }
}