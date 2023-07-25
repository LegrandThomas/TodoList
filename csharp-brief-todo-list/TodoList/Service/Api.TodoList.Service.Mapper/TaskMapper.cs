using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Mapper
{
    public class TaskMapper
    {
        public static Task TransformCreateDTOToEntity(CreateTaskDTO taskDTO)
        {
            return new Api.TodoList.Data.Entity.Model.Task()
            {
                Name = CreateTaskDTO.Name,

                Description = CreateTaskDTO.Description,
                DateCreated = CreateTaskDTO.DateCreated,
                DateDue = CreateTaskDTO.DateDue
            };
        }

        public static ReadTaskDTO TransformEntityToReadDTO(Task task)
        {
            return new ReadTaskDTO
            {
                IdTask = task.IdTask,
                Name = task.Name,
                Description = task.Description,
                DateCreated = task.DateCreated,
                DateDue = task.DateDue
            };
        }
    }
}