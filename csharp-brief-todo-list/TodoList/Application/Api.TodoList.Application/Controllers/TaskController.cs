using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.TodoList.Application.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [Route("api/[controller]"), ApiController, Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Handle get request for retrieving all the tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 200)]
        public async Task<ActionResult> GetTasksAsync() => Ok(await _taskService.GetTasksAsync());

        /// <summary>
        /// Handle get request with params for retrieving a task by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), ProducesResponseType(typeof(ReadTaskDTO), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _taskService.GetTaskByIdAsync(id));

        /// <summary>
        /// Handle get request with params for retrieving a task by his userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("User/{id}"), ProducesResponseType(typeof(ReadTaskDTO), 200)]
        public async Task<ActionResult> GetTasksByUserId(int id) => Ok(await _taskService.GetTasksByUserIdAsync(id));

        /// <summary>
        /// Handle post request for creating a new task
        /// </summary>
        /// <param name="taskDTO"></param>
        /// <returns>Task DTO</returns>
        [HttpPost, ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 200)]
        public async Task<ActionResult> Post([FromBody] CreateTaskDTO taskDTO) => Ok(await _taskService.AddTaskAsync(taskDTO).ConfigureAwait(false));

        /// <summary>
        /// Handle delete request for deleting a task by his id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Task DTO</returns>
        [HttpDelete("{id}"), ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 200)]
        public async Task<ActionResult> Delete(int id) => Ok(await _taskService.RemoveTaskAsync(id).ConfigureAwait(false));
    }
}