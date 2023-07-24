using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.TodoList.Data.Entity.Model
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdStatus { get; set; }
        public string Value { get; set; }
    }
}
