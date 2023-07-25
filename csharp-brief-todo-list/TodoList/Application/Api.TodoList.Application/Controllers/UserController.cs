using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Service.Contract;
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
        [HttpGet, ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<ActionResult> GetUsersAsync() => Ok(await _userService.GetUsersAsync());

        /// <summary>
        /// Handle get request with params for retrieving a user with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> Get(int id) => Ok(await _userService.GetUserByIdAsync(id));
    }
}