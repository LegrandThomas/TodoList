using Api.TodoList.Data.Context.Contract;

namespace Api.TodoList.Data.Repository
{
    public class TaskRepository : Repository<Entity.Model.Task>
    {
        public TaskRepository(ITodoListDbContext dbContext) : base(dbContext)
        {
        }
    }
}