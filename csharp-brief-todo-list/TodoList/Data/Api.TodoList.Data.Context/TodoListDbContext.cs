using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.TodoList.Data.Context
{
    public class TodoListDbContext : DbContext, ITodoListDbContext
    {
        public TodoListDbContext()
        {
        }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Entity.Model.Task> Tasks { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Email).HasColumnType("mediumtext");
                entity.Property(e => e.FirstName).HasColumnType("mediumtext");
                entity.Property(e => e.LastName).HasColumnType("mediumtext");
            });

            modelBuilder.Entity<Entity.Model.Task>(entity =>
            {
                entity.HasKey(e => new { e.IdTask, e.IdUser, e.IdStatus })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("tasks");

                entity.HasIndex(e => e.IdStatus, "IdStatus");

                entity.HasIndex(e => e.IdUser, "IdUser");

                entity.Property(e => e.IdTask).ValueGeneratedOnAdd();
                entity.Property(e => e.DateCreated)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("datetime");
                entity.Property(e => e.DateDue).HasColumnType("datetime");
                entity.Property(e => e.Description).HasColumnType("mediumtext");
                entity.Property(e => e.Name).HasColumnType("mediumtext");

                entity.HasOne(t => t.User).WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

                entity.ToTable("statuses");

                entity.Property(e => e.Value).HasColumnType("mediumtext");
            });
        }
    }
}