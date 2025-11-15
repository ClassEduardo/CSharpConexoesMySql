using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexoesMySql.Conexoes.Dapper.Factory;
using ConexoesMySql.Conexoes.Dapper.RepoBase;
using ConexoesMySql.DTO;
using Dapper;

namespace ConexoesMySql.Conexoes.Dapper.Repo;

public class AlunoRepositoryDapper(
        IDbConnectionFactory connectionFactory,
        string tableName = "Alunos"
    ) : RepositoryBaseDapper<Aluno>(connectionFactory, tableName), IAlunoRepositoryDapper
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<Aluno?> GetAlunoComTurmaAsync(int id)
    {
        using var connection = CreateConnection();

        var sql = @"SELECT 
                        a.*, 
                        t.*
                    FROM Alunos a
                    INNER JOIN Turma t
                    ON a.TurmaId = t.Id
                    WHERE a.Id = @id";

        var result = await connection.QueryAsync<Aluno, Turma, Aluno>(
            sql,
            (aluno, turma) =>
            {
                aluno.Turma = turma;
                return aluno;
            },
            new { Id = id },
            splitOn: "Id"
        );

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<Aluno?>> GetByTurmaIdAsync(int turmaId)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Aluno>(
            $"SELECT * FROM Alunos WHERE TurmaId = @TurmaId",
            new { TurmaId = turmaId }
        );
    }

}