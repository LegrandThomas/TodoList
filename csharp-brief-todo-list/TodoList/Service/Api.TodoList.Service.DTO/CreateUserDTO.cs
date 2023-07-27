namespace Api.TodoList.Service.DTO
{
    public class CreateUserDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public ICollection<CreateTaskDTO> Tasks { get; set; }
    }
}