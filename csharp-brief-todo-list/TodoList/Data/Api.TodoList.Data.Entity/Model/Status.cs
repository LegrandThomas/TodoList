using System.ComponentModel.DataAnnotations;


namespace Api.TodoList.Data.Entity.Model
{

    public class Status
    {
        /// <summary>
        /// Id of the status
        /// </summary>
        [Key]
        public int IdStatus { get; set; }

        /// <summary>
        /// Value of the status 
        /// </summary>
        public string Value { get; set; }
    }
}