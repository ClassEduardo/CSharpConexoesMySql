using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ConexoesMySql.Conexoes.EntityFramework;

[ApiController]
[Route("api/[controller]")]
public class TurmaController(IRepositoryBase<Turma> repository) : ControllerBase
{
    private readonly IRepositoryBase<Turma> _repository = repository;

    /// <summary>
    /// Retorna todas as turmas
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? professor)
    {
        try
        {
            IEnumerable<Turma> turmas;

            if (!string.IsNullOrEmpty(professor))
            {
                turmas = await _repository.GetAllAsync(t => t.Professor.Contains(professor));
            }
            else
            {
                turmas = await _repository.GetAllAsync(null);
            }

            return Ok(turmas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar turmas", error = ex.Message });
        }
    }

    /// <summary>
    /// Retorna uma turma específica por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var turma = await _repository.FindAsync(t => t.Id == id);

            if (turma == null)
            {
                return NotFound(new { message = $"Turma com ID {id} não encontrada" });
            }

            return Ok(turma);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar turma", error = ex.Message });
        }
    }

    /// <summary>
    /// Cria uma nova turma
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Turma turma)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaTurma = await _repository.AddAsync(turma);
            
            return CreatedAtAction(
                nameof(GetByIdAsync), 
                new { id = novaTurma.Id }, 
                novaTurma
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar turma", error = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza uma turma existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Turma turma)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != turma.Id)
            {
                return BadRequest(new { message = "ID da URL não corresponde ao ID da turma" });
            }

            var turmaExistente = await _repository.FindAsync(t => t.Id == id);
            
            if (turmaExistente == null)
            {
                return NotFound(new { message = $"Turma com ID {id} não encontrada" });
            }

            var turmaAtualizada = await _repository.UpdateAsync(turma);
            
            return Ok(turmaAtualizada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao atualizar turma", error = ex.Message });
        }
    }

    /// <summary>
    /// Deleta uma turma por ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var sucesso = await _repository.DeleteAsync(id);
            
            if (!sucesso)
            {
                return NotFound(new { message = $"Turma com ID {id} não encontrada" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao deletar turma", error = ex.Message });
        }
    }

    /// <summary>
    /// Busca turmas por período
    /// </summary>
    [HttpGet("periodo/{periodo}")]
    public async Task<IActionResult> GetByPeriodoAsync(string periodo)
    {
        try
        {
            var turmas = await _repository.GetAllAsync(t => t.Periodo == periodo);
            
            return Ok(turmas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar turmas por período", error = ex.Message });
        }
    }
}