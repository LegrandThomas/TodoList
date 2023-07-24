using Api.TodoList.Data.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.TodoList.Data.Context
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext()
        {

        }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Entity.Model.Task> Tasks { get; set; }
        public virtual DbSet<Status> Status { get; set; }
    }
}