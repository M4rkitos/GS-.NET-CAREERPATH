// Arquivo: ApplicationDbContext.cs
// Localização: CareerPath.Infrastructure/Data/ApplicationDbContext.cs

using CareerPath.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerPath.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets (Representação das tabelas no banco de dados)
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        // Se você tiver a entidade de ligação
        // public DbSet<MatchRecomendacao> MatchRecomendacoes { get; set; }


        // Mapeamentos EF Core (Opcional, mas recomendado para configurar chaves e nomes)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exemplo de Mapeamento: Garantir que o Email é único
            modelBuilder.Entity<Profissional>()
                .HasIndex(p => p.Email)
                .IsUnique();
            
            // Exemplo: Configurar a chave estrangeira (Habilidade -> Curso)
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Habilidade) // Um curso tem uma Habilidade
                .WithMany(h => h.Cursos)  // Uma habilidade tem muitos Cursos
                .HasForeignKey(c => c.HabilidadeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}