namespace Api.TodoList.Service.DTO
{
    public class ReadUserDTO : CreateUserDTO
    {
        public int IdUser { get; set; }
        public ICollection<ReadTaskDTO> Tasks { get; set; }
    }
}