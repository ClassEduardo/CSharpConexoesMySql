using System.Linq.Expressions;

namespace ConexoesMySql.Conexoes.EntityFramework;

// <T> determina que a classe trabalhe com um tipo genérico 
// "where T : class" determina que esse generico seja do tipo class 
public interface IRepositoryBase<T> where T : class
{
    // T é usado para dizer o que os métodos devem retornar

    //Recebe um predicate do tipo Expression<Func<T, bool>>
    //Retorna um IEnumerable<T>
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate);

    //Recebe um predicate do tipo Expression<Func<T, bool>>
    //Retorna um T
    Task<T>? FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    //Recebe um generico T
    //Retorna uma promisse Task e dps um tipo T
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}