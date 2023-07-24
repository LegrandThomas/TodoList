using Api.TodoList.Data.Context;
using Api.TodoList.Data.Repository.Contract;

namespace Api.TodoList.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoListDbContext _dbContext;

        public UserRepository(TodoListDbContext dbContext)
        {
            dbContext = _dbContext;
        }
    }
}