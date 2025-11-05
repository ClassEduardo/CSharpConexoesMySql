using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexoesMySql.DTO;

public class Turma
{
    public int Id { get; set; }
    public string Professor { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Periodo { get; set; } = string.Empty;
}