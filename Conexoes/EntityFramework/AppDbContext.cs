using ConexoesMySql.DTO;
using Microsoft.EntityFrameworkCore;

namespace ConexoesMySql.Conexoes.EntityFramework;

// Declaração de método e classe na mesma declaração 
// Cada DbContext é como se fosse um banco de dados diferente

// Contexto espera receber as opções no construtor
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    //Contrutor diz "Quando alguém me criar AppDbContext
    // Preciso dessas configs"
    public DbSet<Aluno> Aluno { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("Alunos");
                entity.HasKey(a => a.IdAluno);
                entity.Property(a => a.IdAluno).HasColumnName("Id");
                entity.Property(a => a.Nome).IsRequired().HasMaxLength(3);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(255);

                /*
                    Mapeamento Fluent API no DbContext
                    entity.HasOne(a => a.Turma).WithMany().HasForeignKey(a => a.TurmaId);
                */
                entity.HasOne(a => a.Turma)
                          .WithMany()
                          .HasForeignKey(a => a.TurmaId);
            });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.ToTable("Turma");
            entity.HasKey(t => t.Id);
        });
    }
}