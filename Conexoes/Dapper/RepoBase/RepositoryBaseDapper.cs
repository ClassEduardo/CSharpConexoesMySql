using ConexoesMySql.Conexoes.Dapper.Factory;
using Dapper;
using System.Data;
using System.Text.RegularExpressions;

namespace ConexoesMySql.Conexoes.Dapper.RepoBase;

public class RepositoryBaseDapper<T>(IDbConnectionFactory connectionFactory, string tableName) : IRepositoryBaseDapper<T> where T : class
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    private readonly string _tableName = tableName;
    private readonly string _keyColumn = "Id";

    protected IDbConnection CreateConnection()
    {
        return _connectionFactory.CreateConnection();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        using var connection = CreateConnection();

        var sql = $"SELECT * FROM {_tableName} WHERE {_keyColumn} = @Id";
        return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public virtual async Task<int> InsertAsync(T entity)
    {
        using var connection = CreateConnection();

        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != _keyColumn && p.GetValue(entity) != null)
            .ToList();

        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

        var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
        return await connection.ExecuteAsync(sql, entity);
    }

    public virtual async Task<int> UpdateAsync(T entity)
    {
        using var connection = CreateConnection();

        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != _keyColumn)
            .Select(p => $"{p.Name} = @{p.Name}");

        var setClause = string.Join(", ", properties);

        var sql = $"UPDATE {_tableName} SET {setClause} WHERE {_keyColumn} = @{_keyColumn}";
        return await connection.ExecuteAsync(sql, entity);
    }

    public virtual async Task<int> DeleteAsync(object id)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(
            $"DELETE FROM {_tableName} WHERE {_keyColumn} = @Id",
            new { Id = id }
        );
    }
}