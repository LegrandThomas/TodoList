using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.TodoList.Application.Controllers
{
    /// <summary>
    /// Status controller
    /// </summary>
    [Route("api/[controller]"), ApiController, Produces("application/json")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Handle get request for retrieving all the status
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ReadStatusDTO>), 200)]
        public async Task<ActionResult> GetStatusesAsync() => Ok(await _statusService.GetStatusesAsync());

        /// <summary>
        /// Handle get request with params for retrieving a status by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status DTO</returns>
        [HttpGet("{id}"), ProducesResponseType(typeof(ReadStatusDTO), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _statusService.GetStatusByIdAsync(id));
    }
}
