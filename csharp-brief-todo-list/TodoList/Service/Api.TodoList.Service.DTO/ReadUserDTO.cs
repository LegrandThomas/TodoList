namespace Api.TodoList.Service.DTO
{
    public class ReadUserDTO : CreateUserDTO
    {
        public int IdUser { get; set; }

        public List<ReadTaskDTO> Tasks { get; set; }
    }
}