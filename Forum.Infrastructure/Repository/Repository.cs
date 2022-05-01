using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Infrastructure.Repository;

/// <summary>
/// Generic Repository of type <see cref="{T}"/>, extends <see cref="IRepository{T}"/> where T is a class
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="Forum.Infrastructure.Repository.Interfaces.IRepository&lt;T&gt;" />
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// The database set
    /// </summary>
    private readonly DbSet<T> _dbSet;

    /// <summary>
    /// The database
    /// </summary>
    private readonly DbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{T}"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public Repository(DbContext db)
    {
        _db = db;
        _dbSet = db.Set<T>();
    }

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>
    /// Returns a Generic list of <see cref="T:System.Collections.Generic.IEnumerable`1" />
    /// </returns>
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var includeProp in SanitizeProps(includeProperties))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    /// <summary>
    /// Gets the by int identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// Returns a Generic <see cref="!:&lt;T&gt;" /> type
    /// </returns>
    public async Task<T?> GetByIntIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Gets the by unique identifier identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// Returns a Generic <see cref="!:&lt;T&gt;" /> type
    /// </returns>
    public async Task<T?> GetByGuidIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Gets the first or default asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <param name="tracked">if set to <c>true</c> [tracked].</param>
    /// <returns>
    /// Returns a Generic <see cref="{T}" /> type
    /// </returns>
    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool tracked = true)
    {
        if (tracked)
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(filter);
            if (includeProperties == null) return await query.FirstOrDefaultAsync();
            query = SanitizeProps(includeProperties).Aggregate(query, (current, includeProp) => current.Include(includeProp));
            return await query.FirstOrDefaultAsync();
        }
        else
        {
            var query = _dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties == null) return await query.FirstOrDefaultAsync();
            query = SanitizeProps(includeProperties).Aggregate(query, (current, includeProp) => current.Include(includeProp));
            return await query.FirstOrDefaultAsync();
        }
    }

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <summary>
    /// Removes the range.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    /// <summary>
    /// Sanitizes the props.
    /// </summary>
    /// <param name="props">The props.</param>
    /// <returns></returns>
    private static IEnumerable<string> SanitizeProps(string props) => props.Replace(" ", string.Empty).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
}

