using Api.TodoList.Service.DTO;

namespace Api.TodoList.Service.Mapper
{
    public class TaskMapper
    {
        public static Data.Entity.Model.Task TransformCreateDTOToEntity(CreateTaskDTO taskDTO)
        {
            return new Data.Entity.Model.Task
            {
                IdUser = taskDTO.IdUser,
                IdStatus = taskDTO.IdStatus,
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                DateCreated = taskDTO.DateCreated,
                DateDue = taskDTO.DateDue
            };
        }

        public static ReadTaskDTO TransformEntityToReadDTO(Data.Entity.Model.Task task)
        {
            return new ReadTaskDTO
            {
                IdTask = task.IdTask,
                IdUser = task.IdUser,
                IdStatus = task.IdStatus,
                Name = task.Name,
                Description = task.Description,
                DateCreated = task.DateCreated,
                DateDue = task.DateDue
            };
        }
    }
}