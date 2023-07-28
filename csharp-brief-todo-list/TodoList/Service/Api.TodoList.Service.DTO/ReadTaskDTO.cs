namespace Api.TodoList.Service.DTO
{
    public class ReadTaskDTO : CreateTaskDTO
    {
        public int IdTask { get; set; }

        public string DateCreated { get; set; }
    }
}