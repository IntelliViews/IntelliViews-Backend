using IntelliViews.Data;

namespace IntelliViews.Repository
{
    public interface IRepository<T> where T : DbEntity
    {
        /// <summary>
        /// Gets an entity by its id
        /// </summary>
        /// <param name="id">The unique id of the entity</param>
        /// <returns>The specified entity</returns>
        Task<T> GetById(string id);
        /// <summary>
        /// Gets all entities from the context
        /// </summary>
        /// <returns>A list of all entities</returns>
        Task<List<T>> GetAll();
        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">The entity it will be updated to</param>
        /// <returns>The updated entity</returns>
        Task<T> Update(T entity);
        /// <summary>
        /// Deletes an entity by its id
        /// </summary>
        /// <param name="id">The unique id of the entity</param>
        /// <returns>The deleted entity</returns>
        Task<T> DeleteById(string id);
        /// <summary>
        /// Creates an entity in the context
        /// </summary>
        /// <param name="entity">The entity to be created</param>
        /// <returns>The created entity</returns>
        Task<T> Create(T entity);
    }
}
