namespace ConexoesMySql.DTO;
public class Aluno
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public string? Email { get; set; }
    public char Genero { get; set; }
    public int TurmaId { get; set; }
}