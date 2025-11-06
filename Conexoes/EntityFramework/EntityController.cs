using System.Linq.Expressions;
using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ConexoesMySql.Conexoes.EntityFramework;
[ApiController]
[Route("api/entityFrameworkControlle")]
public class EntityController(IRepositoryBase<Aluno> repository) : ControllerBase
{
    private readonly IRepositoryBase<Aluno> _repository = repository;

    [HttpGet]
    public async Task<IActionResult>? GetAlunoByNomeAsync([FromQuery] string nome)
    {
        var aluno = await _repository.FindAsync(a => a.Nome == nome);
        return Ok(aluno);
    }
}