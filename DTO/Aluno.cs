using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexoesMySql.DTO;
public class Aluno
{
    public int IdAluno { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public string? Email { get; set; }
    public char Genero { get; set; }
    public int? TurmaId { get; set; }
    public Turma? Turma { get; set; }  // Propriedade de navegação
}