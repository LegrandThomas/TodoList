using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.TodoList.Application.Controllers
{
    [Route("api/[controller]"), ApiController, Produces("application/json")]
    public class StatusController : ControllerBase
    {

        private readonly IStatusService _statusService;

        /// <summary>
        /// Controller of status
        /// </summary>
        /// <param name="statusService"></param>
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }


        // GET: api/<StatusController>
        /// <summary>
        /// Get all Status
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ReadStatusDTO>), 200)]
        public async Task<ActionResult> GetStatusesAsync() => Ok(await _statusService.GetStatusesAsync());

        // get One Status by Id
        [HttpGet("{id}"), ProducesResponseType(typeof(ReadStatusDTO), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _statusService.GetStatusByIdAsync(id));
    }
}
