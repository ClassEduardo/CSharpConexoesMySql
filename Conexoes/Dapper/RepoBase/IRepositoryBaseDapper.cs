using System.Linq.Expressions;

namespace ConexoesMySql.Conexoes.Dapper.RepoBase;

public interface IRepositoryBaseDapper<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(object id);

}