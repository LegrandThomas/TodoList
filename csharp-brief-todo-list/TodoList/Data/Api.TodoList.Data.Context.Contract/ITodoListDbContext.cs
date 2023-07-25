using Api.TodoList.Data.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.TodoList.Data.Context.Contract
{
    public interface ITodoListDbContext : IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Entity.Model.Task> Tasks { get; set; }
        DbSet<Status> Statuses { get; set; }
    }
}
