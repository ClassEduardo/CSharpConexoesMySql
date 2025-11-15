using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConexoesMySql.DTO;
public class Aluno
{
    [Key]
    [JsonPropertyName("idAluno")] 
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public string? Email { get; set; }
    public char Genero { get; set; }
    public int? TurmaId { get; set; }
    public Turma? Turma { get; set; } 
}