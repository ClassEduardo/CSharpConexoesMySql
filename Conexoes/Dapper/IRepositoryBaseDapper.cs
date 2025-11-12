using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConexoesMySql.Conexoes.Dapper;

public interface IRepositoryBaseDapper<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(object id);
}