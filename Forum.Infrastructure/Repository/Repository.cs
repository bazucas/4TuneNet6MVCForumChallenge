using Forum.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _db;
    public Repository(DbContext db)
    {
        _db = db;
        _dbSet = db.Set<T>();
    }

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

    public async Task<T?> GetByIntIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetByGuidIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
    {
        if (tracked)
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in SanitizeProps(includeProperties))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
        else
        {
            var query = _dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in SanitizeProps(includeProperties))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    private static IEnumerable<string> SanitizeProps(string props) => props.Replace(" ", string.Empty).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
}

