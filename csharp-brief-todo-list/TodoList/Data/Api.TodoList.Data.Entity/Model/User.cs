using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.TodoList.Data.Entity.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}