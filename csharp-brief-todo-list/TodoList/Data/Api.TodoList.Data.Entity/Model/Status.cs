using System.ComponentModel.DataAnnotations;
<<<<<<< Updated upstream
=======

namespace Api.TodoList.Data.Entity.Model;
>>>>>>> Stashed changes

namespace Api.TodoList.Data.Entity.Model
{
<<<<<<< Updated upstream
    public class Status
    {
        [Key]
        public int IdStatus { get; set; }
=======

    /// <summary>
    /// Id of the status
    /// </summary>
    [Key]
    public int IdStatus { get; set; }
>>>>>>> Stashed changes

        public string Value { get; set; } = null!;
    }
}