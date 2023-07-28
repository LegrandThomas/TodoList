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
      [HttpGet("{id}")]
      [ProducesResponseType(typeof(ReadTaskDTO), 200)]
      [ProducesResponseType(typeof(string), 404)] // 404 Not Found
      [ProducesResponseType(typeof(string), 500)] // 500 Internal Server Error
        public async Task<ActionResult> Get(int id)
            {
                 try
                    {
                    var task = await _taskService.GetTaskByIdAsync(id);
                    if (task == null)
                       {
                         return NotFound("La tâche n'a pas été trouvée."); // 404 Not Found
                       }

                        return Ok(task); // 200 OK
                       }
                        catch (Exception ex)
                        {
              
                        return NotFound("La tâche n'a pas été trouvée."); // 404 Not Found
                    }
                }

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
                return Conflict(new { message = "Erreur lors de la création de la tâche : Un champ en double existe déjà." });
            }
        }

        /// <summary>
        /// Handle put request for updating a task
        /// </summary>
        /// <param name="taskDTO"></param>
        /// <returns>Task DTO</returns>
        [HttpPut, ProducesResponseType(typeof(ReadTaskDTO), 201)]
        public async Task<ActionResult> Put([FromBody] string taskJSON)
        {
            if (string.IsNullOrEmpty(taskJSON))
                return BadRequest("La tâche est vide ou nulle.");
            // todo json to dto ?
            return null;
            // return Ok(await _taskService.UpdateTaskAsync(taskDTO).ConfigureAwait(false));
        }


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
                return StatusCode(500, new { message = "Erreur interne du serveur lors de la suppression de la tâche" });
            }
        }

        // Méthode privée pour gérer les exceptions et renvoyer des réponses HTTP appropriées.
       
    }
}
