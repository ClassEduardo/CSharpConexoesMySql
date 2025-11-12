using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConexoesMySql.Conexoes.Dapper;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}