using System;

namespace CareerPath.Domain.Entities
{
    public class Profissional
    {
        // Construtor para aplicar a invariante
        public Profissional(string nomeCompleto, string email, string profissaoAtual, int anosExperiencia)
        {
            // Invariante 1: Email não pode ser nulo ou vazio
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("O email do profissional é obrigatório.");
            }
            // Invariante 2: Anos de Experiência não pode ser negativo
            if (anosExperiencia < 0)
            {
                throw new ArgumentException("Os anos de experiência não podem ser negativos.");
            }
            
            this.Id = Guid.NewGuid();
            this.NomeCompleto = nomeCompleto;
            this.Email = email;
            this.ProfissaoAtual = profissaoAtual;
            this.AnosExperiencia = anosExperiencia;
            this.DataCadastro = DateTime.UtcNow;
            // A coleção HistóricoRecomendacoes será inicializada no EF Core
        }

        public Guid Id { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Email { get; private set; } 
        public string ProfissaoAtual { get; private set; }
        public int AnosExperiencia { get; private set; } 
        public DateTime DataCadastro { get; private set; }
        
        // Relacionamento (para a MatchRecomendacao)
        // public virtual ICollection<MatchRecomendacao> HistoricoRecomendacoes { get; set; } 

        // Método de atualização de profissão (Exemplo de Regra de Negócio de Serviço)
        public void AtualizarProfissao(string novaProfissao, int novosAnosExperiencia)
        {
            if (novosAnosExperiencia < this.AnosExperiencia)
            {
                throw new InvalidOperationException("Não é possível diminuir a experiência registrada.");
            }
            this.ProfissaoAtual = novaProfissao;
            this.AnosExperiencia = novosAnosExperiencia;
        }
    }
}