using System.ComponentModel.DataAnnotations;

namespace Api.TodoList.Data.Entity.Model
{
    /// <summary>
    /// Represent our table users
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id Utilisateur
        /// </summary>
        [Key]
        public int IdUser { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Prénom
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Tâches
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; } = new List<Model.Task>();
    }
}