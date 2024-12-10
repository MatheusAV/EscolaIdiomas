using EscolaIdiomas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaIdiomas.Infrastructure.Data.Context
{
    public class EscolaIdiomasContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        public EscolaIdiomasContext(DbContextOptions<EscolaIdiomasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Aluno>()
                .HasIndex(a => a.Cpf).IsUnique();

            modelBuilder.Entity<Turma>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Matricula>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Matricula>()
                .HasIndex(m => new { m.AlunoId, m.TurmaId }).IsUnique();

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId);
        }
    }
}