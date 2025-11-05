using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConexoesMySql.Conexoes.EntityFramework;

// Quando contexto é solicitado toda config feita entre program e AppDbContext
public class RepositoryBase<T>(AppDbContext context) : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _DbSet = context.Set<T>();


    // .net ve que precisa cria um AppDbContext
    // Olha pro constructor "Preciso do DbContextOptions<AppDbContext>"
    //Busca no container
    //Cria o AppDbContext passando as configs de program e da própria classe AppDbContext
    //Injeta no controller
    public virtual async Task<T>? FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _DbSet.FirstOrDefaultAsync(predicate);
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