using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexoesMySql.Conexoes.Dapper.Repo;
using ConexoesMySql.Conexoes.Dapper.RepoBase;
using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ConexoesMySql.Conexoes.Dapper;

[ApiController]
[Route("api/[Controller]")]
public class DapperController(IRepositoryBaseDapper<Turma> _repositoryBaseTurma, IAlunoRepositoryDapper _alunoRepository) : ControllerBase
{
    private readonly IRepositoryBaseDapper<Turma> repositoryBaseTurma = _repositoryBaseTurma;
    private readonly IAlunoRepositoryDapper repositoryAlunoBase = _alunoRepository;

    [HttpGet("/Turma")]
    public async Task<IActionResult> GetAllTurma()
    {
        var turmas = await repositoryBaseTurma.GetAllAsync(); 
        return Ok(turmas);
    }

    [HttpGet("/AlunoById")]
    public async Task<IActionResult> GetAll([FromQuery] int alunoId)
    {
        var aluno = await repositoryAlunoBase.GetByIdAsync(alunoId); 
        return Ok(aluno);
    }

    [HttpGet("/AlunoAndTurma")]
    public async Task<IActionResult> GetAllAlunoAndTurma([FromQuery] int turmaId)
    {
        var turmas = await repositoryAlunoBase.GetByTurmaIdAsync(turmaId); 
        return Ok(turmas);
    }
}