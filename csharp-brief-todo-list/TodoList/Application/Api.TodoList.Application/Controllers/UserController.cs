using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.Contract;
using Api.TodoList.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.TodoList.Application.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]"), ApiController, Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Handle get request for retrieving all the users
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ReadUserDTO>), 200)]
        public async Task<ActionResult> GetUsersAsync() => Ok(await _userService.GetUsersAsync());

        /// <summary>
        /// Handle get request with params for retrieving a user with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _userService.GetUserByIdAsync(id));

        /// <summary>
        /// Handle post request for creating a new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>User DTO</returns>
        [HttpPost, ProducesResponseType(typeof(IEnumerable<ReadUserDTO>), 200)]
        public async Task<ActionResult> Post([FromBody] CreateUserDTO userDTO) => Ok(await _userService.AddUserAsync(userDTO).ConfigureAwait(false));

        /// <summary>
        /// Handle delete request for deleting a user by his id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User DTO</returns>
        [HttpDelete("{id}"), ProducesResponseType(typeof(IEnumerable<ReadUserDTO>), 200)]
        public async Task<ActionResult> Delete(int id) => Ok(await _userService.RemoveUserAsync(id).ConfigureAwait(false));
    }
}