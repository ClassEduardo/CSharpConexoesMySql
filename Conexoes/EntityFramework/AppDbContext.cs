using ConexoesMySql.DTO;
using Microsoft.EntityFrameworkCore;

namespace ConexoesMySql.Conexoes.EntityFramework;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Aluno> Aluno { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>()
            .ToTable("Alunos")
            .HasKey(a => a.Id);
    }
}