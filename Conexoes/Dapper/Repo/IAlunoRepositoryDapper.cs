using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexoesMySql.Conexoes.Dapper.RepoBase;
using ConexoesMySql.DTO;

namespace ConexoesMySql.Conexoes.Dapper.Repo;

public interface IAlunoRepositoryDapper : IRepositoryBaseDapper<Aluno>
{
    Task<IEnumerable<Aluno?>> GetByTurmaIdAsync(int turmaId);
    Task<Aluno?> GetAlunoComTurmaAsync(string id);
}