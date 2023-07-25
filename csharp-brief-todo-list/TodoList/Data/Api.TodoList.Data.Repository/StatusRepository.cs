using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Entity.Model;

namespace Api.TodoList.Data.Repository
{
    public class StatusRepository : Repository<Status>
    {
        public StatusRepository(ITodoListDbContext dbContext) : base(dbContext)
        {
        }
    }
}