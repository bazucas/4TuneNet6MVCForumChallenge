using System.Linq.Expressions;

namespace Forum.Infrastructure.Repository.Interfaces;

/// <summary>
/// Generic Repository interface of type <see cref="{T}"/> where T is a class.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>Returns a Generic list of <see cref="IEnumerable{T}"/></returns>
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    /// <summary>
    /// Gets the by int identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns a Generic <see cref="{T}"/> type</returns>
    Task<T?> GetByIntIdAsync(int id);

    /// <summary>
    /// Gets the by unique identifier identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns a Generic <see cref="{T}"/> type</returns>
    Task<T?> GetByGuidIdAsync(Guid id);

    /// <summary>
    /// Gets the first or default asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <param name="tracked">if set to <c>true</c> [tracked].</param>
    /// <returns>Returns a Generic <see cref="{T}"/> type</returns>
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool tracked = true);

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Update(T entity);

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Remove(T entity);

    /// <summary>
    /// Removes the range.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void RemoveRange(IEnumerable<T> entities);
}

