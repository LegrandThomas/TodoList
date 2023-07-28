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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("User/{userId}"), ProducesResponseType(typeof(ReadTaskDTO), 200)]
        public async Task<ActionResult> GetTasksByUserId(int userId) => Ok(await _taskService.GetTasksByUserIdAsync(userId));

        /// <summary>
        /// Handle get request with params for retrieving a task by his statusId
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpGet("User/{userId}/Status/{statusId}"), ProducesResponseType(typeof(ReadTaskDTO), 200)]
        public async Task<ActionResult> GetTasksByStatusId(int statusId, int userId) => Ok(await _taskService.GetTasksByStatusIdAsync(statusId, userId));

        /// <summary>
        /// Handle post request for creating a new task
        /// </summary>
        /// <param name="taskDTO"></param>
        /// <returns>Task DTO</returns>
        [HttpPost, ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 201)]
        public async Task<ActionResult> Post([FromBody] CreateTaskDTO taskDTO)
        {
            try
            {
                var createdTask = await _taskService.AddTaskAsync(taskDTO).ConfigureAwait(false);
                Console.WriteLine(createdTask);
                return CreatedAtAction(nameof(Get), new { id = createdTask.IdTask }, createdTask);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Handle put request for updating a task
        /// </summary>
        /// <param name="taskDTO"></param>
        /// <returns>Task DTO</returns>
        [HttpPut, ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 201)]
        public async Task<ActionResult> Put([FromBody] CreateTaskDTO taskDTO) => Ok(await _taskService.UpdateTaskAsync(taskDTO).ConfigureAwait(false));


        /// <summary>
        /// Handle delete request for deleting a task by his id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Task DTO</returns>
        [HttpDelete("{id}"), ProducesResponseType(typeof(IEnumerable<ReadTaskDTO>), 200)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deletedTask = await _taskService.RemoveTaskAsync(id).ConfigureAwait(false);
                return Ok(deletedTask);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // Méthode privée pour gérer les exceptions et renvoyer des réponses HTTP appropriées.
        private ActionResult HandleException(Exception ex)
        {
           
                // Si c'est une exception de duplicata, renvoyez une réponse 409 (Conflict) avec un message d'erreur approprié.
                return Conflict(new { message = "Erreur lors de la création de la tâche : Un champ en double existe déjà." });
           
        }
    }
}
