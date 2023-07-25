using System.ComponentModel.DataAnnotations;

namespace Api.TodoList.Data.Entity.Model
{
    public class Status
    {
        [Key]
        public int IdStatus { get; set; }

        public string Value { get; set; } = null!;
    }
}