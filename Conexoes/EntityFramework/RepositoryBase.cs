using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConexoesMySql.Conexoes.EntityFramework;

// Quando contexto é solicitado toda config feita entre program e AppDbContext
public class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T>? FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query
            .Where(predicate)
            .FirstOrDefaultAsync();
    }

    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}