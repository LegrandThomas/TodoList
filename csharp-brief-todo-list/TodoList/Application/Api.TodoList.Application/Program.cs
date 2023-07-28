using System.Reflection;
using Api.TodoList.Data.Context;
using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository;
using Api.TodoList.Data.Repository.Contract;
using Api.TodoList.Service;
using Api.TodoList.Service.Contract;
using Microsoft.EntityFrameworkCore;

namespace Api.TodoList.Application
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContext<ITodoListDbContext, TodoListDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            builder.Services.AddAutoMapper(Assembly.Load("Api.TodoList.Service.Mapper"));

            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Data.Entity.Model.Task>, TaskRepository>(); 
            builder.Services.AddScoped<IRepository<Status>, StatusRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IStatusService, StatusService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Ajouter la gestion des CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                    

                });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Ajouter le middleware CORS avant le middleware UseAuthorization
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}