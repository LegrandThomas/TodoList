using System.ComponentModel;

namespace Api.TodoList.Service.DTO
{
    public class CreateTaskDTO
    {
        [DefaultValue(1)]
        public int IdUser { get; set; }

        [DefaultValue(1)]
        public int IdStatus { get; set; }

        [DefaultValue("Ma nouvelle tâche")]
        public string? Name { get; set; }

        [DefaultValue("")]
        public string? Description { get; set; }


        [DefaultValue("")]
        public string? DateCreated { get; set; }

        [DefaultValue("")]
        public string? DateDue { get; set; }
    }
}