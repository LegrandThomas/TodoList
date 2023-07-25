using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Api.TodoList.Data.Context.Contract;
using Api.TodoList.Data.Repository.Contract;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Api.TodoList.Data.Repository
{
    /// <summary>
    /// Base class for all our repository
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ITodoListDbContext _dbContext;

        protected DbSet<T> Entities => _dbContext.Set<T>();

        protected Repository(ITodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Return all the entities of type T, can pass Include with the includes parameter eg: 'GetAll(client => client.Address)'
        /// </summary>
        /// <returns>List of entities of type T</returns>
        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = Entities.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Return null or the entity of type T with the corresponding Id, can pass Include with the includes parameter eg: 'GetById(1, client => client.Address)'
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity of type T</returns>
        /// <exception cref="InvalidOperationException">Throw InvalidOperationException if entity of type T does not have a Id property</exception>
        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            // not used anymore since we used name like IdUser, IdTask ...
            // var idProperty = typeof(T).GetProperty("Id");
            var idProperty = typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<KeyAttribute>() != null);
            if (idProperty == null)
            {
                // throw new InvalidOperationException($"Entity {typeof(T)} does not have an 'Id' property.");
                throw new InvalidOperationException($"Entity {typeof(T)} does not have an [Key] attribute on one property.");
            }

            var parameter = Expression.Parameter(typeof(T), "entity");
            // var property = Expression.Property(parameter, "Id");
            var property = Expression.Property(parameter, idProperty);
            var equality = Expression.Equal(property, Expression.Constant(id));

            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            var query = Entities.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(lambda).ConfigureAwait(false);
        }

        /// <summary>
        /// Add entity of type T to the database
        /// </summary>
        /// <param name="entity">Entity of type T</param>
        /// <returns>Entity of type T</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<T> Add(T entity)
        {
            // should not happen ?
            if (entity.GetType() != typeof(T))
            {
                throw new InvalidOperationException("Invalid entity type");
            }

            var elementAdded = await Entities.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return elementAdded.Entity;
        }

        /// <summary>
        /// Remove entity of type T from the database
        /// </summary>
        /// <param name="entity">Entity of type T</param>
        /// <returns>Entity of type T</returns>
        public async Task<T> Remove(T entity)
        {
            var elementDeleted = Entities.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return elementDeleted.Entity;
        }
    }
}