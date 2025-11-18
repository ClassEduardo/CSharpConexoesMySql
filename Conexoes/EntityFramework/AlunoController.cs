using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ConexoesMySql.Conexoes.EntityFramework;

[ApiController]
[Route("api/[controller]")]
public class AlunoController(IRepositoryBase<Aluno> repository) : ControllerBase
{
    private readonly IRepositoryBase<Aluno> _repository = repository;

    /// <summary>
    /// Retorna todos os alunos ou filtra por parâmetros
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? nome)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retorna um aluno específico por ID com seus relacionamentos
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Busca aluno por nome exato com a turma relacionada
    /// </summary>
    [HttpGet("buscar")]
    public async Task<IActionResult> GetByNomeAsync([FromQuery] string nome)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Cria um novo aluno
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Aluno aluno)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Atualiza um aluno existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Aluno aluno)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deleta um aluno por ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retorna alunos por turma
    /// </summary>
    [HttpGet("turma/{turmaId}")]
    public async Task<IActionResult> GetByTurmaAsync(int turmaId)
    {
        throw new NotImplementedException();
    }
}