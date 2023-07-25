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


        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }


        // GET: api/<StatusController>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ReadStatusDTO>), 200)]
        public async Task<ActionResult> GetStatusesAsync() => Ok(await _statusService.GetStatusesAsync());

        // GET api/<StatusController>/5
        [HttpGet("{id}"), ProducesResponseType(typeof(ReadStatusDTO), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _statusService.GetStatusByIdAsync(id));

        // POST api/<StatusController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
