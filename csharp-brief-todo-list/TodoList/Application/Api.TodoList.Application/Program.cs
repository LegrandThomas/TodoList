using Api.TodoList.Data.Context;
using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service;
using Api.TodoList.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.TodoList.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContext<ITodoListDbContext, TodoListDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Data.Entity.Model.Task>, TaskRepository>();
            builder.Services.AddScoped<IRepository<Status>, StatusRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IStatusService, StatusService>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(Assembly.Load("Api.TodoList.Service.Mapper"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}