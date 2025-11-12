using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexoesMySql.Conexoes.Dapper;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("ConnectionStrings") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada");
    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}