
using Api.TodoList.Data.Context;
using Api.TodoList.Data.Entity.Model;
using Api.TodoList.Data.Repository;
using Api.TodoList.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Api.TodoList.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            // Add services to the container.
            builder.Services.AddDbContext<TodoListDbContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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