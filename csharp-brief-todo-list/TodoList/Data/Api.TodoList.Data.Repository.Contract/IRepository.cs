using System.Linq.Expressions;

namespace Api.TodoList.Data.Repository.Contract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);

        Task<T> Add(T entity);

        Task<T> Remove(T entity);
    }
}