using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConexoesMySql.Conexoes.EntityFramework;

public class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
    {
        try
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Erro ao buscar {ex.Message}", ex);
        }
    }

    public virtual async Task<T>? FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        try
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .Where(predicate)
                .FirstOrDefaultAsync() ?? throw new Exception("Falha ao obter o registro.");
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Erro ao buscar registro: {ex.Message}", ex);
        }
    }

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Erro ao adicionar registro: {ex.Message}", ex);
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Erro ao atualizar registro: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);

            if(entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Erro ao deletar registro: {ex.Message}", ex);
        }
    }
}